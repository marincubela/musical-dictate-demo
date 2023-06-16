namespace Application.Exercises.Commands.CreateExercise;

public class CreateExerciseCommandPartDto
{
    public int Start { get; set; }
    public int End { get; set; }
    public int AllowedTimeInSeconds { get; set; }
    public int AllowedNumberOfAttempts { get; set; }
}