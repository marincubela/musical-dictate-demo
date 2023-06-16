using Domain.Common;

namespace Domain.ValueObjects;

public class Grade : ValueObject
{
    private Grade(int value, string name)
    {
        Value = value;
        Name = name;
    }

    public int Value { get; }

    public string Name { get; }

    public static Grade One => new(1, "Nedovoljan");
    public static Grade Two => new(2, "Dovoljan");
    public static Grade Three => new(3, "Dobar");
    public static Grade Four => new(4, "Vrlo dobar");
    public static Grade Five => new(5, "Odličan");

    private static readonly Dictionary<int, Grade> Grades = new()
    {
        {One.Value, One},
        {Two.Value, Two},
        {Three.Value, Three},
        {Four.Value, Four},
        {Five.Value, Five},
    };

    public static Grade FromValue(int value)
    {
        if (Grades.TryGetValue(value, out Grade? grade))
            return grade;

        throw new Exception();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Name;
    }
}