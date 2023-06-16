using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Teachers.Queries.GetTeachers;

public class GetTeachersDto : IMapFrom<Teacher>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}