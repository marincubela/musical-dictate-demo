using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentSolutions.Queries.GetStudentSolutionsByAssignment;

public class GetStudentSolutionsByAssignmentTeacherDto : IMapFrom<Teacher>
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}