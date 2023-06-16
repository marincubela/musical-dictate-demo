using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.StudentGroups.Commands.CreateStudentGroup;

public class CreateStudentGroupCommand : IRequest<CreateStudentGroupDto>
{
    public string Name { get; set; }
}

public class CreateStudentGroupCommandHandler : IRequestHandler<CreateStudentGroupCommand, CreateStudentGroupDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public CreateStudentGroupCommandHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<CreateStudentGroupDto> Handle(CreateStudentGroupCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _context.Teachers
            .SingleOrDefaultAsync(teacher => teacher.Id == _currentUserService.UserId, cancellationToken);

        var group = _context.StudentGroups
            .Add(StudentGroup.Create(request.Name, teacher));

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CreateStudentGroupDto>(group.Entity);
    }
}