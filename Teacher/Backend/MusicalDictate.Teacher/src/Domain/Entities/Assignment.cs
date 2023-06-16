using Domain.Common;

namespace Domain.Entities;

public class Assignment : BaseAuditableEntity
{
    private Assignment() { }

    public Exercise Exercise { get; private set; }

    public string ExerciseId { get; private set; } = Guid.Empty.ToString();

    public StudentGroup StudentGroup { get; private set; }

    public string StudentGroupId { get; private set; }

    public Teacher Teacher { get; private set; }

    public string TeacherId { get; private set; }
    
    public string GraderType { get; private set; }

    public ICollection<StudentSolution> StudentSolutions { get; private set; } = new List<StudentSolution>();

    public void Update(Exercise exercise, StudentGroup studentGroup, Teacher teacher, string graderType)
    {
        this.Exercise = exercise;
        this.StudentGroup = studentGroup;
        this.Teacher = teacher;
        this.GraderType = graderType;
    }

    public static Assignment Create(Exercise exercise, StudentGroup studentGroup, Teacher teacher, string graderType)
    {
        return new Assignment {Exercise = exercise, StudentGroup = studentGroup, Teacher = teacher, GraderType = graderType};
    }
}