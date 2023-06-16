using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentGroups.Queries.GetStudentGroups;

public class GetStudentGroupsDto : IMapFrom<StudentGroup>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string TeacherId { get; set; }
    public List<GetStudentGroupsStudentDto> Students { get; set; }
    public List<GetStudentGroupsAssignmentDto> Assignments { get; set; }
}