using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentGroups.Queries.GetStudentGroups;

public class GetStudentGroupsStudentDto : IMapFrom<Student>
{
    public string Id { get; set; }
    public string Jmbag { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NameClass { get; set; }
}