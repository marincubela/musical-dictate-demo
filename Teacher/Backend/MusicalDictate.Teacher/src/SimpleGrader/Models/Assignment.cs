
using System.Collections.Generic;

namespace SimpleGrader.Models;

public class Assignment
{
    private Assignment() { }

    public Exercise Exercise { get; private set; }

    public StudentGroup StudentGroup { get; private set; }

    public string StudentGroupId { get; private set; }

    public Teacher Teacher { get; private set; }

    public string TeacherId { get; private set; }
    
    public string GraderType { get; private set; }

    public ICollection<StudentSolution> StudentSolutions { get; private set; } = new List<StudentSolution>();
}