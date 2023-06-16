using Domain.Common;

namespace Domain.Entities;

public class Student : BaseAuditableEntity
{
    public string Jmbag { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string NameClass { get; set; } = string.Empty;
    
    public ICollection<StudentGroup> MyGroups = new List<StudentGroup>();

    public ICollection<StudentSolution> StudentSolutions = new List<StudentSolution>();

    public static Student Create(string id, string jmbag, string firstName, string lastName, string nameClass)
    {
        return new Student {Id = id, FirstName = firstName, LastName = lastName, Jmbag = jmbag, NameClass = nameClass};
    }
}