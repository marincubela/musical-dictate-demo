using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentGroups.Queries.GetMyStudentGroup;

public class GetMyStudentGroupExerciseDto : IMapFrom<Exercise>
{
    public string Id { get; set; }
    public string Title { get; set; }
    public GetMyStudentGroupTeacherDto Teacher { get; set; }
    public DateTime CreatedUtc { get; set; }
}