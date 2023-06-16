namespace Application.Exercises.Commands.UpdateExercise;

public class UpdateExerciseCommandPartDto
{
    public int Start { get; set; }
    public int End { get; set; }
    public int AllowedTimeInSeconds { get; set; }
    public int AllowedNumberOfAttempts { get; set; }
}