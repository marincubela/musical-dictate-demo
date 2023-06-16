using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Teachers.Queries.GetTeacher;

public class GetTeacherDto : IMapFrom<Teacher>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}