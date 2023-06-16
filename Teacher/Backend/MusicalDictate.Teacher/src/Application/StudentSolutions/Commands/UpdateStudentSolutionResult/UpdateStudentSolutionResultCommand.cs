using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.StudentSolutions.Commands.UpdateStudentSolutionResult;

public class UpdateStudentSolutionResultCommand : IRequest
{
    public string StudentSolutionId { get; set; }
    public int Grade { get; set; }
    public double Percentage { get; set; }
    public string Comment { get; set; }
}

public class UpdateStudentSolutionResultCommandHandler : IRequestHandler<UpdateStudentSolutionResultCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateStudentSolutionResultCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(UpdateStudentSolutionResultCommand request, CancellationToken cancellationToken)
    {
        var solution = await _context.StudentSolutions
            .Include(solution => solution.Assignment)
            .SingleOrDefaultAsync(solution => solution.Id == request.StudentSolutionId, cancellationToken);

        if (solution == null)
            throw new NotFoundException(nameof(StudentSolution), request.StudentSolutionId);

        var teacher = await _context.Teachers
            .SingleOrDefaultAsync(teacher => teacher.Id == _currentUserService.UserId, cancellationToken);

        solution.UpdateResult(Result.Create(teacher, Grade.FromValue(request.Grade), request.Percentage, request.Comment));

        solution.AddDomainEvent(new StudentSolutionResultUpdatedEvent(solution));
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}