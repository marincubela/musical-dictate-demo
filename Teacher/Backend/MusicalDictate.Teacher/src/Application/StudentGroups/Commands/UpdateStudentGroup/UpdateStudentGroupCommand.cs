using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.StudentGroups.Commands.UpdateStudentGroup;

public class UpdateStudentGroupCommand : IRequest
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<UpdateStudentGroupCommandAssignmentDto> Assignments { get; set; } = new();
    public List<string>? StudentIds { get; set; }
}

public class UpdateStudentGroupCommandHandler : IRequestHandler<UpdateStudentGroupCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateStudentGroupCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(UpdateStudentGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _context.StudentGroups
            .Include(group => group.Students)
            .Include(group => group.Assignments)
            .SingleOrDefaultAsync(group => group.Id == request.Id, cancellationToken);

        if (group == null)
            throw new NotFoundException(nameof(StudentGroup), request.Id);

        group.Update(request.Name);

        // Update students
        if (request.StudentIds != null)
        {
            group.ClearStudents();
            foreach (string studentId in request.StudentIds)
            {
                var student = await _context.Students
                    .SingleOrDefaultAsync(student => student.Id == studentId, cancellationToken);

                if (student == null)
                    throw new NotFoundException(nameof(Student), request.Id);

                group.AddStudent(student);
            }
        }

        var teacher = await _context.Teachers
            .SingleOrDefaultAsync(teacher => teacher.Id == _currentUserService.UserId, cancellationToken);

        // Update assignments
        var oldAssignments = request.Assignments
            .Where(assignment => assignment.Id != null)
            .ToList();

        // Remove deleted assignments
        var assignments = group.Assignments.ToList().AsReadOnly();

        foreach (var assignment in assignments)
        {
            if (oldAssignments.All(a => a.Id != assignment.Id))
            {
                _context.Assignments.Remove(assignment);
            }
        }

        // Check exercises and create new assignments
        foreach (var assignment in request.Assignments.Where(assignment => assignment.Id == null).ToList())
        {
            var exercise = await _context.Exercises
                .SingleOrDefaultAsync(exercise => exercise.Id == assignment.ExerciseId, cancellationToken);

            if (exercise == null)
                throw new NotFoundException(nameof(Exercise), assignment.ExerciseId);
            
            group.AddAssignment(Assignment.Create(exercise, group, teacher, assignment.GraderType));
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}