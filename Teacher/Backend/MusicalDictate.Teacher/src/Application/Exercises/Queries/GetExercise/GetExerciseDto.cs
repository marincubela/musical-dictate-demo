using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Exercises.Queries.GetExercise;

public class GetExerciseDto : IMapFrom<Exercise>
{
    public string Id { get; set; }
    public string Title { get; set; }
    public GetExerciseSheetDto Solution { get; set; }
    public GetExerciseTeacherDto Teacher { get; set; }
    public DateTime CreatedUtc { get; set; }
    public IEnumerable<GetExercisePartDto> Parts { get; set; }
}