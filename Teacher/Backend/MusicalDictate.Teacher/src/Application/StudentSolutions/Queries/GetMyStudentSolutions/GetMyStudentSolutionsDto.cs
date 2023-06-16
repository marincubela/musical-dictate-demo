using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentSolutions.Queries.GetMyStudentSolutions;

public class GetMyStudentSolutionsDto : IMapFrom<StudentSolution>
{
    public string Id { get; set; }
    public GetMyStudentSolutionsResultDto? Result { get; set; }
    public DateTime CreatedUtc { get; set; }
}