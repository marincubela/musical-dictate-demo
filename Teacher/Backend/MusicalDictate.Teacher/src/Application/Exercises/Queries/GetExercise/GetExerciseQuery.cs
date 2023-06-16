using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Exercises.Queries.GetExercise;

public record GetExerciseQuery(string Id) : IRequest<GetExerciseDto>;

public class GetExerciseQueryHandler : IRequestHandler<GetExerciseQuery, GetExerciseDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetExerciseQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetExerciseDto> Handle(GetExerciseQuery request, CancellationToken cancellationToken)
    {
        var exerciseDto = await _context.Exercises
            .Include(exercise => exercise.Teacher)
            .Include(exercise => exercise.Solution)
            .Include(exercise => exercise.Parts)
            .Where(exercise => exercise.Id == request.Id)
            .ProjectTo<GetExerciseDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(cancellationToken);

        if (exerciseDto == null)
            throw new NotFoundException(nameof(Exercise), request.Id);

        return exerciseDto;
    }
}