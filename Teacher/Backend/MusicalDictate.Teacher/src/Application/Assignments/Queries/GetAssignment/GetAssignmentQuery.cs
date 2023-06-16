using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Assignments.Queries.GetAssignment;

public record GetAssignmentQuery(string Id) : IRequest<GetAssignmentDto>;

public class GetAssignmentQueryHandler : IRequestHandler<GetAssignmentQuery, GetAssignmentDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAssignmentQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetAssignmentDto> Handle(GetAssignmentQuery request, CancellationToken cancellationToken)
    {
        var assignmentDto = await _context.Assignments
            .Include(assignment => assignment.Exercise).ThenInclude(exercise => exercise.Teacher)
            .Include(assignment => assignment.Exercise).ThenInclude(exercise => exercise.Solution)
            .Include(assignment => assignment.Exercise).ThenInclude(exercise => exercise.Parts)
            .Where(assignment => assignment.Id == request.Id)
            .ProjectTo<GetAssignmentDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(cancellationToken);

        if (assignmentDto == null)
            throw new NotFoundException(nameof(Assignment), request.Id);

        return assignmentDto;
    }
}