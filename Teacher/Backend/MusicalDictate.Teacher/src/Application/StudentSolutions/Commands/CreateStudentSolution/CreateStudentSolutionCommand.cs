using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.StudentSolutions.Commands.CreateStudentSolution;

public class CreateStudentSolutionCommand : IRequest<string>
{
    public string AssignmentId { get; set; }
    public IFormFile Solution { get; set; }
}

public class CreateStudentSolutionCommandHandler : IRequestHandler<CreateStudentSolutionCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateStudentSolutionCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<string> Handle(CreateStudentSolutionCommand request, CancellationToken cancellationToken)
    {
        var student = await _context.Students
            .SingleOrDefaultAsync(student => student.Id == _currentUserService.UserId, cancellationToken);

        Sheet? sheet = null;
        await using (var memoryStream = new MemoryStream())
        {
            await request.Solution.CopyToAsync(memoryStream, cancellationToken);

            // Upload the file if less than 2 MB
            if (memoryStream.Length < 2097152)
            {
                sheet = Sheet.Create(memoryStream.ToArray());
            }
        }

        var assignment = await _context.Assignments
            .SingleOrDefaultAsync(assignment => assignment.Id == request.AssignmentId, cancellationToken);

        if (assignment == null)
            throw new NotFoundException(nameof(Assignment), request.AssignmentId);

        var studentSolution = StudentSolution.Create(student, assignment, sheet);
        _context.StudentSolutions.Add(studentSolution);
        
        studentSolution.AddDomainEvent(new StudentSolutionCreatedEvent(studentSolution));

        await _context.SaveChangesAsync(cancellationToken);

        return studentSolution.Id;
    }
}