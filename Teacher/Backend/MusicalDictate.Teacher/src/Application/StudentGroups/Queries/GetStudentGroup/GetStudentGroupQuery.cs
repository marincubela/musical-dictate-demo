using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.StudentGroups.Queries.GetStudentGroup;

public record GetStudentGroupQuery(string Id) : IRequest<GetStudentGroupDto>;

public class GetStudentGroupQueryHandler : IRequestHandler<GetStudentGroupQuery, GetStudentGroupDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetStudentGroupQueryHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<GetStudentGroupDto> Handle(GetStudentGroupQuery request, CancellationToken cancellationToken)
    {
        await _context.Exercises.LoadAsync(cancellationToken);
        await _context.Teachers.LoadAsync(cancellationToken);
        
        var group = await _context.StudentGroups
            .Include(group => group.Students)
            .Include(group => group.Assignments)
            .SingleOrDefaultAsync(group => group.Id == request.Id, cancellationToken);

        if (group == null)
            throw new NotFoundException(nameof(StudentGroup), request.Id);

        return _mapper.Map<GetStudentGroupDto>(group);
    }
}