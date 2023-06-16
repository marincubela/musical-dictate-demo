using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.StudentSolutions.Queries.GetMyStudentSolutions;

public record GetMyStudentSolutionsQuery(string assignmentId) : IRequest<IEnumerable<GetMyStudentSolutionsDto>>;

public class GetMyStudentSolutionsQueryHandler : IRequestHandler<GetMyStudentSolutionsQuery, IEnumerable<GetMyStudentSolutionsDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public GetMyStudentSolutionsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IMapper mapper)
    {
        _context = context;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetMyStudentSolutionsDto>> Handle(GetMyStudentSolutionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.StudentSolutions
            .Include(solution => solution.Result)
            .Where(solution => solution.StudentId == _currentUserService.UserId)
            .Where(solution => solution.AssignmentId == request.assignmentId)
            .ProjectTo<GetMyStudentSolutionsDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}