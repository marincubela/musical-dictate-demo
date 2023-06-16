using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Assignments.Queries.GetAssignments;

public record GetAssignmentsQuery : IRequest<IEnumerable<GetAssignmentsDto>>;

public class GetAssignmentsQueryHandler : IRequestHandler<GetAssignmentsQuery, IEnumerable<GetAssignmentsDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAssignmentsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetAssignmentsDto>> Handle(GetAssignmentsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Assignments
            .Include(a => a.StudentGroup)
            .Include(a => a.Teacher)
            .ProjectTo<GetAssignmentsDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}