using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Exercises.Commands.CreateExercise;

public class CreateExerciseDto : IMapFrom<Exercise>
{
    public string Id { get; set; }
    public string Title { get; set; }
}