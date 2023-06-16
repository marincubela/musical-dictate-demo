using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Students.Queries.GetStudentsByGroup;

public class GetStudentsByGroupDto : IMapFrom<Student>
{
    public string Jmbag { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NameClass { get; set; }
}