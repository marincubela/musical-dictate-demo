using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentGroups.Commands.CreateStudentGroup;

public class CreateStudentGroupDto : IMapFrom<StudentGroup>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string TeacherId { get; set; }
}