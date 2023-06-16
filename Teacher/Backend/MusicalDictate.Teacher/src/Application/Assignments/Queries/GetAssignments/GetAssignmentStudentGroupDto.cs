using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Assignments.Queries.GetAssignments;

public class GetAssignmentStudentGroupDto : IMapFrom<StudentGroup>
{
    public string Id { get; set; }
    public string Name { get; set; }
}