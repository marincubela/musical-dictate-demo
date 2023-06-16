using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.StudentGroups.Queries.GetMyStudentGroup;

public record GetMyStudentGroupQuery(string Id) : IRequest<GetMyStudentGroupDto>;

public class GetMyStudentGroupQueryHandler : IRequestHandler<GetMyStudentGroupQuery, GetMyStudentGroupDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetMyStudentGroupQueryHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<GetMyStudentGroupDto> Handle(GetMyStudentGroupQuery request, CancellationToken cancellationToken)
    {
        await _context.Exercises.LoadAsync(cancellationToken);
        await _context.Teachers.LoadAsync(cancellationToken);
        
        var group = await _context.StudentGroups
            .Include(group => group.Students)
            .Include(group => group.Assignments).ThenInclude(assignment => assignment.StudentSolutions)
            .SingleOrDefaultAsync(group => group.Id == request.Id, cancellationToken);

        if (group == null)
            throw new NotFoundException(nameof(StudentGroup), request.Id);

        return _mapper.Map<GetMyStudentGroupDto>(group);
    }
}