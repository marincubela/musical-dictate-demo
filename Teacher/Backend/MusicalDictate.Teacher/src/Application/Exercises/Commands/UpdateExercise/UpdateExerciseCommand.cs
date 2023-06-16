using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Application.Exercises.Commands.UpdateExercise;

public class UpdateExerciseCommand : IRequest
{
    public string Id { get; set; }
    public string? Title { get; set; } = null;
    public IFormFile Sheet { get; set; }
    public string? Parts { get; set; }
}

public class UpdateExerciseCommandHandler : IRequestHandler<UpdateExerciseCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateExerciseCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateExerciseCommand request, CancellationToken cancellationToken)
    {
        var exercise = await _context.Exercises
            .Include(exercise => exercise.Parts)
            .SingleOrDefaultAsync(exercise => exercise.Id == request.Id, cancellationToken);

        if (exercise == null)
            throw new NotFoundException(nameof(Exercise), request.Id);

        Sheet? sheet = null;
        await using (var memoryStream = new MemoryStream())
        {
            await request.Sheet.CopyToAsync(memoryStream, cancellationToken);

            // Upload the file if less than 2 MB
            if (memoryStream.Length < 2097152)
            {
                sheet = Sheet.Create(memoryStream.ToArray());
            }
        }

        var parts = request.Parts != null
            ? JsonConvert.DeserializeObject<IEnumerable<Part>>(request.Parts)
            : null;

        exercise.Update(request.Title, parts, sheet);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}