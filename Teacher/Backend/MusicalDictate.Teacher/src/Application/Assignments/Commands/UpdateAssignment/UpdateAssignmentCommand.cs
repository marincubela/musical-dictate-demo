using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Assignments.Commands.UpdateAssignment;

public class UpdateAssignmentCommand : IRequest
{
    public string Id { get; set; }
    public string? ExerciseId { get; set; } = null;
    public string? StudentGroupId { get; set; } = null;
    public string? TeacherId { get; set; } = null;
    public string? GraderType { get; set; } = null;
}

public class UpdateAssignmentCommandHandler : IRequestHandler<UpdateAssignmentCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateAssignmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateAssignmentCommand request, CancellationToken cancellationToken)
    {
        var assignment = await _context.Assignments
            .Include(assignment => assignment.Exercise)
            .Include(assignment => assignment.StudentGroup)
            .SingleOrDefaultAsync(assignment => assignment.Id == request.Id, cancellationToken);

        if (assignment == null)
            throw new NotFoundException(nameof(Assignment), request.Id);


        var exercise = request.ExerciseId != null
            ? await _context.Exercises
                .SingleOrDefaultAsync(exercise => exercise.Id == request.ExerciseId, cancellationToken)
            : assignment.Exercise;

        if (exercise == null)
            throw new NotFoundException(nameof(Exercise), request.ExerciseId ?? "null");

        var group = request.StudentGroupId != null
            ? await _context.StudentGroups
                .SingleOrDefaultAsync(group => group.Id == request.StudentGroupId, cancellationToken)
            : assignment.StudentGroup;

        if (group == null)
            throw new NotFoundException(nameof(StudentGroup), request.StudentGroupId ?? "null");

        var teacher = request.TeacherId != null
            ? await _context.Teachers
                .SingleOrDefaultAsync(teacher => teacher.Id == request.TeacherId, cancellationToken)
            : assignment.Teacher;

        if (teacher == null)
            throw new NotFoundException(nameof(StudentGroup), request.TeacherId ?? "null");

        var graderType = request.GraderType ?? assignment.GraderType;

        assignment.Update(exercise, group, teacher, graderType);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}