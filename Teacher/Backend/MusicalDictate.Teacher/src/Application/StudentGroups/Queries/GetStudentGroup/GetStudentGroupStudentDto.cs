using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentGroups.Queries.GetStudentGroup;

public class GetStudentGroupStudentDto : IMapFrom<Student>
{
    public string Id { get; set; }
    public string Jmbag { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NameClass { get; set; }
}