using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentGroups.Queries.GetStudentGroup;

public class GetStudentGroupExerciseDto : IMapFrom<Exercise>
{
    public string Id { get; set; }
    public string Title { get; set; }
    public GetStudentGroupTeacherDto Teacher { get; set; }
    public DateTime CreatedUtc { get; set; }
}