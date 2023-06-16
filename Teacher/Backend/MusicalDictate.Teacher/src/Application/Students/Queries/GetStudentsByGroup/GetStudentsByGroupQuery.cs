using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Students.Queries.GetStudentsByGroup;

public record GetStudentsByGroupQuery(string GroupId) : IRequest<IEnumerable<GetStudentsByGroupDto>>;

public class GetStudentsByGroupQueryHandler : IRequestHandler<GetStudentsByGroupQuery, IEnumerable<GetStudentsByGroupDto>>
{
    private readonly IIdentityService _identityService;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetStudentsByGroupQueryHandler(IIdentityService identityService, IMapper mapper, IApplicationDbContext context)
    {
        _identityService = identityService;
        _mapper = mapper;
        _context = context;
    }

    public async Task<IEnumerable<GetStudentsByGroupDto>> Handle(GetStudentsByGroupQuery request, CancellationToken cancellationToken)
    {
        return await _context.Students
            .Include(student => student.MyGroups)
            .Where(student => student.MyGroups.Select(g => g.Id).Contains(request.GroupId))
            .ProjectTo<GetStudentsByGroupDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}