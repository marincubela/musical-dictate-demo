using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Teachers.Queries.GetTeacher;

public record GetTeacherQuery(string Id) : IRequest<GetTeacherDto>;

public class GetTeacherQueryHandler : IRequestHandler<GetTeacherQuery, GetTeacherDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTeacherQueryHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<GetTeacherDto> Handle(GetTeacherQuery request, CancellationToken cancellationToken)
    {
        var teacher = await _context.Teachers
            .SingleOrDefaultAsync(teacher => teacher.Id == request.Id, cancellationToken);

        if (teacher == null)
            throw new NotFoundException(nameof(Teacher), request.Id);

        return _mapper.Map<GetTeacherDto>(teacher);
    }
}