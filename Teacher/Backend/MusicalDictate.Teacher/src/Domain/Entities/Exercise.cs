using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Exercise : BaseAuditableEntity
{
    private Exercise() { }

    public string Title { get; private set; } = string.Empty;

    public string TeacherId { get; private set; } = Guid.Empty.ToString();
    public Teacher Teacher { get; private set; }

    public ICollection<Part> Parts { get; private set; } = new List<Part>();

    public string? SolutionId { get; private set; }

    public Sheet? Solution { get; private set; }

    public static Exercise Create(string title, Teacher teacher, IEnumerable<Part> parts, Sheet? solution = null)
    {
        return new Exercise {Title = title, Teacher = teacher, Solution = solution, Parts = parts.ToList()};
    }

    public void Update(string? title = null, IEnumerable<Part>? parts = null, Sheet? solution = null)
    {
        if (title != null)
            Title = title;

        if (parts != null)
        {
            Parts.Clear();
            foreach (Part part in parts)
            {
                Parts.Add(part);
            }
        }

        if (solution != null)
        {
            Solution = solution;
        }
    }
}