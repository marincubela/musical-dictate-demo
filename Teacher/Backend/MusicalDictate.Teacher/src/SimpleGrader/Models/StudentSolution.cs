using System;

namespace SimpleGrader.Models;

public class StudentSolution
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
}