namespace SimpleGrader.Models;

public class Grade
{
    private Grade(int value, string name)
    {
        Value = value;
        Name = name;
    }

    public int Value { get; }

    public string Name { get; }
}