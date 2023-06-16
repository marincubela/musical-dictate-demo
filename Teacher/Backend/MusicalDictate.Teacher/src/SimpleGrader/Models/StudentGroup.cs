using System.Collections.Generic;

namespace SimpleGrader.Models;

public class StudentGroup
{
    public string Id { get; set; }

    public string Name { get; private set; } = string.Empty;

    public Teacher Teacher { get; private set; }

    public string TeacherId { get; private set; }

    public ICollection<Student> Students { get; } = new List<Student>();

    public ICollection<Assignment> Assignments { get; } = new List<Assignment>();
}