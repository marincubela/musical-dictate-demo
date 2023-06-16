using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Students.Queries.GetStudents;

public record GetStudentsQuery : IRequest<IEnumerable<GetStudentsDto>>;

public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, IEnumerable<GetStudentsDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetStudentsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetStudentsDto>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Students
            .ProjectTo<GetStudentsDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}