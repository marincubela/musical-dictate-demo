using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Teachers.Queries.GetTeachers;

public record GetTeachersQuery : IRequest<IEnumerable<GetTeachersDto>>;

public class GetTeachersQueryHandler : IRequestHandler<GetTeachersQuery, IEnumerable<GetTeachersDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTeachersQueryHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<IEnumerable<GetTeachersDto>> Handle(GetTeachersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Teachers
            .ProjectTo<GetTeachersDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}