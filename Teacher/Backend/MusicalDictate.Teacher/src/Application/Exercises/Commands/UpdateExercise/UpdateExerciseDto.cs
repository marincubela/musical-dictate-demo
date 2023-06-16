using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Exercises.Commands.UpdateExercise;

public class UpdateExerciseDto : IMapFrom<Exercise>
{
    public string Id { get; set; }
    public string Title { get; set; }
}