using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Assignments.Commands.DeleteAssignment;

public record DeleteAssignmentCommand(string Id) : IRequest;

public class DeleteAssignmentCommandHandler : IRequestHandler<DeleteAssignmentCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteAssignmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteAssignmentCommand request, CancellationToken cancellationToken)
    {
        var assignment = await _context.Assignments
            .SingleOrDefaultAsync(assignment => assignment.Id == request.Id, cancellationToken);

        if (assignment == null)
            return Unit.Value;

        _context.Assignments.Remove(assignment);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}