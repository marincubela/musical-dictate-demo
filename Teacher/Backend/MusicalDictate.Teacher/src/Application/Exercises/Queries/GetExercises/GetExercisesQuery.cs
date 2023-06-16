using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Exercises.Queries.GetExercises;

public record GetExercisesQuery : IRequest<IEnumerable<GetExercisesDto>>;

public class GetExercisesQueryHandler : IRequestHandler<GetExercisesQuery, IEnumerable<GetExercisesDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetExercisesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetExercisesDto>> Handle(GetExercisesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Exercises
            .Include(exercise => exercise.Teacher)
            .ProjectTo<GetExercisesDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}