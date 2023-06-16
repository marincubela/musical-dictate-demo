using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentSolutions.Queries.GetStudentSolution;

public class GetStudentSolutionStudentDto : IMapFrom<Student>
{
    public string Id { get; set; }
    public string Jmbag { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}