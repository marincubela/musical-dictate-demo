using System.Collections.Generic;

namespace SimpleGrader.Models;

public class Exercise
{
    public string Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public Teacher Teacher { get; set; }

    public ICollection<Part> Parts { get; set; } = new List<Part>();

    public Sheet? Solution { get; set; }
}