using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentGroups.Queries.GetStudentGroup;

public class GetStudentGroupDto : IMapFrom<StudentGroup>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string TeacherId { get; set; }
    public List<GetStudentGroupStudentDto> Students { get; set; }
    public List<GetStudentGroupAssignmentDto> Assignments { get; set; }
}