using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentGroups.Queries.GetMyStudentGroup;

public class GetMyStudentGroupStudentSolutionDto : IMapFrom<StudentSolution>
{
    public string Id { get; set; }
}