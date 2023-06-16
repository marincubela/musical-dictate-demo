using Domain.Common;

namespace Domain.Entities;

public class Part : BaseEntity
{
    public int Start { get; set; }
    public int End { get; set; }
    public int AllowedTimeInSeconds { get; set; }
    public int AllowedNumberOfAttempts { get; set; }

    public Exercise Exercise { get; private set; }

    public string ExerciseId { get; private set; }
}