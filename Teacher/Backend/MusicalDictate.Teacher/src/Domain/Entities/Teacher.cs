using Domain.Common;

namespace Domain.Entities;

public class Teacher : BaseAuditableEntity
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
    
    public ICollection<StudentGroup> StudentGroups { get; } = new List<StudentGroup>();
    
    public ICollection<Assignment> Assignments { get; } = new List<Assignment>();

    public ICollection<Exercise> Exercises { get; } = new List<Exercise>();

    public ICollection<StudentSolution> StudentSolutions { get; } = new List<StudentSolution>();

    public ICollection<Result> Results { get; } = new List<Result>();

    public static Teacher Create(string id, string firstName, string lastName)
    {
        return new Teacher {Id = id, FirstName = firstName, LastName = lastName};
    }
}