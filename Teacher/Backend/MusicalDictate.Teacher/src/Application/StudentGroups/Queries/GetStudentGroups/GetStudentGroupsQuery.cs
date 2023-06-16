using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.StudentGroups.Queries.GetStudentGroups;

public record GetStudentGroupsQuery : IRequest<IEnumerable<GetStudentGroupsDto>>;

public class GetStudentGroupsQueryHandler : IRequestHandler<GetStudentGroupsQuery, IEnumerable<GetStudentGroupsDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetStudentGroupsQueryHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<IEnumerable<GetStudentGroupsDto>> Handle(GetStudentGroupsQuery request, CancellationToken cancellationToken)
    {
        return await _context.StudentGroups
            .Include(group => group.Students)
            .Include(group => group.Assignments).ThenInclude(assignment => assignment.Exercise)
            .ProjectTo<GetStudentGroupsDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}