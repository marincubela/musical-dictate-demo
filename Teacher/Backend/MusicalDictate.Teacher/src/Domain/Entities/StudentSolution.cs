using Domain.Common;

namespace Domain.Entities;

public class StudentSolution : BaseAuditableEntity
{
    private StudentSolution() { }

    public Student Student { get; private set; }

    public string StudentId { get; private set; } = Guid.Empty.ToString();

    public string AssignmentId { get; private set; } = Guid.Empty.ToString();

    public Assignment Assignment { get; private set; }

    public string SolutionId { get; private set; } = Guid.Empty.ToString();

    public Sheet Solution { get; private set; }
    
    public Result? Result { get; private set; }
    
    public string? ResultId { get; private set; }

    public void UpdateResult(Result result)
    {
        Result = result;
    }

    public static StudentSolution Create(Student student, Assignment assignment, Sheet solution)
    {
        return new StudentSolution {Student = student, Assignment = assignment, Solution = solution};
    }
}