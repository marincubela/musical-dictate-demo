using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Assignments.Queries.GetAssignment;

public class GetAssignmentExerciseDto : IMapFrom<Exercise>
{
    public string Id { get; set; }
    public string Title { get; set; }
    public GetAssignmentSheetDto Solution { get; set; }
    public GetAssignmentTeacherDto Teacher { get; set; }
    public DateTime CreatedUtc { get; set; }
    public IEnumerable<GetAssignmentPartDto> Parts { get; set; }
}