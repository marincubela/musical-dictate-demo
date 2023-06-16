using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentGroups.Queries.GetMyStudentGroup;

public class GetMyStudentGroupDto : IMapFrom<StudentGroup>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string TeacherId { get; set; }
    public List<GetMyStudentGroupStudentDto> Students { get; set; }
    public List<GetMyStudentGroupAssignmentDto> Assignments { get; set; }
}