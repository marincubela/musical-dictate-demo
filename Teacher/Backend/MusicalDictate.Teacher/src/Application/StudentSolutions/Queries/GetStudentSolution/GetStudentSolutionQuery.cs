using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.StudentSolutions.Queries.GetStudentSolution;

public record GetStudentSolutionQuery(string Id) : IRequest<GetStudentSolutionDto>;

public class GetStudentSolutionQueryHandler : IRequestHandler<GetStudentSolutionQuery, GetStudentSolutionDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetStudentSolutionQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetStudentSolutionDto> Handle(GetStudentSolutionQuery request, CancellationToken cancellationToken)
    {
        var solution = await _context.StudentSolutions
            .Include(solution => solution.Result)
            .Include(solution => solution.Student)
            .Include(solution => solution.Solution)
            .Include(solution => solution.Assignment).ThenInclude(assignment => assignment.Exercise).ThenInclude(exercise => exercise.Solution)
            .Include(solution => solution.Assignment).ThenInclude(assignment => assignment.Exercise).ThenInclude(exercise => exercise.Teacher)
            .Include(solution => solution.Assignment).ThenInclude(assignment => assignment.Exercise).ThenInclude(exercise => exercise.Parts)
            .SingleOrDefaultAsync(solution => solution.Id == request.Id, cancellationToken);

        if (solution == null)
            throw new NotFoundException(nameof(StudentSolution), request.Id);

        return _mapper.Map<GetStudentSolutionDto>(solution);
    }
}