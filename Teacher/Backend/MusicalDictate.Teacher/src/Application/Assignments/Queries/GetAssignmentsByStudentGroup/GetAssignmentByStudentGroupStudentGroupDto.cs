using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Assignments.Queries.GetAssignmentsByStudentGroup;

public class GetAssignmentByStudentGroupStudentGroupDto : IMapFrom<StudentGroup>
{
    public string Id { get; set; }
    public string Name { get; set; }
}