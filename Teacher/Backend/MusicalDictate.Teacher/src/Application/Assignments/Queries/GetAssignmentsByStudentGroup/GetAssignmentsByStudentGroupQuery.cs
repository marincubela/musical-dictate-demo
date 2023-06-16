using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Assignments.Queries.GetAssignmentsByStudentGroup;

public record GetAssignmentsByStudentGroupQuery(string StudentGroupId) : IRequest<IEnumerable<GetAssignmentsByStudentGroupDto>>;

public class GetAssignmentsQueryHandler : IRequestHandler<GetAssignmentsByStudentGroupQuery, IEnumerable<GetAssignmentsByStudentGroupDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAssignmentsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetAssignmentsByStudentGroupDto>> Handle(GetAssignmentsByStudentGroupQuery request, CancellationToken cancellationToken)
    {
        return await _context.Assignments
            .Include(a => a.Exercise)
            .Include(a => a.StudentGroup)
            .Include(a => a.Teacher)
            .Where(assignment => assignment.StudentGroupId == request.StudentGroupId)
            .ProjectTo<GetAssignmentsByStudentGroupDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}