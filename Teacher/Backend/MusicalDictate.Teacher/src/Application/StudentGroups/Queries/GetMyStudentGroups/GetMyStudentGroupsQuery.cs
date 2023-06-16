using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.StudentGroups.Queries.GetMyStudentGroups;

public record GetMyStudentGroupsQuery : IRequest<IEnumerable<GetMyStudentGroupsDto>>;

public class GetMyStudentGroupsQueryHandler : IRequestHandler<GetMyStudentGroupsQuery, IEnumerable<GetMyStudentGroupsDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public GetMyStudentGroupsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IMapper mapper)
    {
        _context = context;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetMyStudentGroupsDto>> Handle(GetMyStudentGroupsQuery request, CancellationToken cancellationToken)
    {
        var student = await _context.Students
            .Include(student => student.MyGroups)
            .SingleOrDefaultAsync(student => student.Id == _currentUserService.UserId, cancellationToken);

        if (student == null)
            throw new NotFoundException(nameof(Student), _currentUserService.UserId);

        return student.MyGroups
            .AsQueryable()
            .ProjectTo<GetMyStudentGroupsDto>(_mapper.ConfigurationProvider)
            .ToList();
    }
}