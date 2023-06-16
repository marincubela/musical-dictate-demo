using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.StudentSolutions.Queries.GetStudentSolutionsByAssignment;

public record GetStudentSolutionsByAssignmentQuery(string assignmentId) : IRequest<IEnumerable<GetStudentSolutionsByAssignmentDto>>;

public class GetStudentSolutionsByAssignmentQueryHandler : IRequestHandler<GetStudentSolutionsByAssignmentQuery, IEnumerable<GetStudentSolutionsByAssignmentDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetStudentSolutionsByAssignmentQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetStudentSolutionsByAssignmentDto>> Handle(GetStudentSolutionsByAssignmentQuery request, CancellationToken cancellationToken)
    {
        return await _context.StudentSolutions
            .Include(solution => solution.Result)
            .Include(solution => solution.Student)
            .Include(solution => solution.Solution)
            .Include(solution => solution.Assignment).ThenInclude(assignment => assignment.Exercise).ThenInclude(exercise => exercise.Solution)
            .Include(solution => solution.Assignment).ThenInclude(assignment => assignment.Exercise).ThenInclude(exercise => exercise.Teacher)
            .Include(solution => solution.Assignment).ThenInclude(assignment => assignment.Exercise).ThenInclude(exercise => exercise.Parts)
            .Where(solution => solution.AssignmentId == request.assignmentId)
            .ProjectTo<GetStudentSolutionsByAssignmentDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}