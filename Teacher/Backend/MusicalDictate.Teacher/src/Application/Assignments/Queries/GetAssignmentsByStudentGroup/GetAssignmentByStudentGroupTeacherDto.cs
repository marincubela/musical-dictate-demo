using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Assignments.Queries.GetAssignmentsByStudentGroup;

public class GetAssignmentByStudentGroupTeacherDto : IMapFrom<Teacher>
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}