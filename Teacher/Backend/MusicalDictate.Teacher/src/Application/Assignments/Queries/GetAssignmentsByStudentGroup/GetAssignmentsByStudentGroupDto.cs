using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Assignments.Queries.GetAssignmentsByStudentGroup;

public class GetAssignmentsByStudentGroupDto : IMapFrom<Assignment>
{
    public string Id { get; set; }
    public string GraderType { get; set; }
    public GetAssignmentByStudentGroupExerciseDto Exercise { get; set; }
    public GetAssignmentByStudentGroupStudentGroupDto StudentGroup { get; set; }
    public GetAssignmentByStudentGroupTeacherDto Teacher { get; set; }
    public DateTime CreatedUtc { get; set; }
}