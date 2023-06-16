using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Assignments.Commands.CreateAssignment;

public class CreateAssignmentCommand : IRequest<CreateAssignmentDto>
{
    public string ExerciseId { get; set; }
    public string StudentGroupId { get; set; }
    public string TeacherId { get; set; }
    public string GradeType { get; set; }
}

public class CreateAssignmentCommandHandler : IRequestHandler<CreateAssignmentCommand, CreateAssignmentDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateAssignmentCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CreateAssignmentDto> Handle(CreateAssignmentCommand request, CancellationToken cancellationToken)
    {
        var exercise = await _context.Exercises
            .SingleOrDefaultAsync(exercise => exercise.Id == request.ExerciseId, cancellationToken);

        if (exercise == null)
            throw new NotFoundException(nameof(Exercise), request.ExerciseId);

        var group = await _context.StudentGroups
            .SingleOrDefaultAsync(group => group.Id == request.StudentGroupId, cancellationToken);

        if (group == null)
            throw new NotFoundException(nameof(StudentGroup), request.StudentGroupId);

        var teacher = await _context.Teachers
            .SingleOrDefaultAsync(teacher => teacher.Id == request.TeacherId, cancellationToken);

        if (teacher == null)
            throw new NotFoundException(nameof(Teacher), request.TeacherId);
            
        var entity = _context.Assignments.Add(Assignment.Create(exercise, group, teacher, request.GradeType));

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CreateAssignmentDto>(entity.Entity);
    }
}