using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Result : BaseAuditableEntity
{
    private Result() { }

    public Grade Grade { get; private set; }

    public Teacher? Teacher { get; private set; }

    public string? TeacherId { get; private set; }

    public string Comment { get; private set; }

    public double Percentage { get; private set; }

    public static Result Create(Teacher? teacher, Grade grade, double percentage, string comment)
    {
        return new Result {Teacher = teacher, Grade = grade, Percentage = percentage, Comment = comment};
    }
}