using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Students.Queries.GetStudent;

public record GetStudentQuery(string Id) : IRequest<GetStudentDto>;

public class GetStudentQueryHandler : IRequestHandler<GetStudentQuery, GetStudentDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetStudentQueryHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<GetStudentDto> Handle(GetStudentQuery request, CancellationToken cancellationToken)
    {
        var student = await _context.Students
            .SingleOrDefaultAsync(student => student.Id == request.Id, cancellationToken);

        if (student == null)
            throw new NotFoundException(nameof(Student), request.Id);

        return _mapper.Map<GetStudentDto>(student);
    }
}