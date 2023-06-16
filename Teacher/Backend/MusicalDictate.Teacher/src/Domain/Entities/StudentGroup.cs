using Domain.Common;

namespace Domain.Entities;

public class StudentGroup : BaseAuditableEntity
{
    private StudentGroup() { }

    public string Name { get; private set; } = string.Empty;

    public Teacher Teacher { get; private set; }

    public string TeacherId { get; private set; }

    public ICollection<Student> Students { get; } = new List<Student>();

    public ICollection<Assignment> Assignments { get; } = new List<Assignment>();

    public void AddStudent(Student student)
    {
        Students.Add(student);
    }

    public bool RemoveStudent(Student student)
    {
        return Students.Remove(student);
    }

    public void ClearStudents()
    {
        Students.Clear();
    }

    public void AddAssignment(Assignment assignment)
    {
        Assignments.Add(assignment);
    }

    public bool RemoveAssignment(Assignment assignment)
    {
        return Assignments.Remove(assignment);
    }

    public void ClearAssignments()
    {
        Assignments.Clear();
    }

    public static StudentGroup Create(string name, Teacher teacher)
    {
        return new StudentGroup {Name = name, Teacher = teacher};
    }

    public void Update(string name)
    {
        Name = name;
    }
}