using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Assignments.Queries.GetAssignments;

public class GetAssignmentsDto : IMapFrom<Assignment>
{
    public string Id { get; set; }
    public string GraderType { get; set; }
    public GetAssignmentStudentGroupDto StudentGroup { get; set; }
    public GetAssignmentTeacherDto Teacher { get; set; }
    public DateTime CreatedUtc { get; set; }
}