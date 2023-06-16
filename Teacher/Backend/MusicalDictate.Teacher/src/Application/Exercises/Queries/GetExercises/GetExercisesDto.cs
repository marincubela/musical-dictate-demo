using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Exercises.Queries.GetExercises;

public class GetExercisesDto : IMapFrom<Exercise>
{
    public string Id { get; set; }
    public string Title { get; set; }
    public GetExercisesTeacherDto Teacher { get; set; }
    public DateTime CreatedUtc { get; set; }
}