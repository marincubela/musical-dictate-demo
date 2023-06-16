using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentGroups.Queries.GetMyStudentGroups;

public class GetMyStudentGroupsDto : IMapFrom<StudentGroup>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string TeacherId { get; set; }
}