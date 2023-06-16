using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.StudentGroups.Commands.DeleteStudentGroup;

public record DeleteStudentGroupCommand(string Id) : IRequest;

public class DeleteStudentGroupCommandHandler : IRequestHandler<DeleteStudentGroupCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteStudentGroupCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteStudentGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _context.StudentGroups.SingleOrDefaultAsync(group => group.Id == request.Id, cancellationToken);

        if (group == null)
            throw new NotFoundException(nameof(StudentGroup), request.Id);

        _context.StudentGroups.Remove(group);
        
        return Unit.Value;
    }
}