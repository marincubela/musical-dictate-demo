using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Exercises.Queries.GetExercise;

public class GetExercisePartDto : IMapFrom<Part>
{
    public int Start { get; set; }
    public int End { get; set; }
    public int AllowedTimeInSeconds { get; set; }
    public int AllowedNumberOfAttempts { get; set; }
}