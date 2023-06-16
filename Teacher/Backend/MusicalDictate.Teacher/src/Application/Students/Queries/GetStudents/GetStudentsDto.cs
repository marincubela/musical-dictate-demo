using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Students.Queries.GetStudents;

public class GetStudentsDto : IMapFrom<Student>
{
    public string Id { get; set; }
    public string Jmbag { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NameClass { get; set; }
}