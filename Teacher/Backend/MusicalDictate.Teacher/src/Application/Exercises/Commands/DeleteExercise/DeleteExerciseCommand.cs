using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Exercises.Commands.DeleteExercise;

public record DeleteExerciseCommand(string Id) : IRequest;

public class DeleteExerciseCommandHandler : IRequestHandler<DeleteExerciseCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteExerciseCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
    {
        var exercise = await _context.Exercises
            .SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (exercise == null)
            return Unit.Value;

        _context.Exercises.Remove(exercise);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}