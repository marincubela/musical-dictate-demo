using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentSolutions.Queries.GetStudentSolution;

public class GetStudentSolutionDto : IMapFrom<StudentSolution>
{
    public string Id { get; set; }
    public GetStudentSolutionStudentDto Student { get; set; }
    public GetStudentSolutionAssignmentDto Assignment { get; set; }
    public GetStudentSolutionResultDto? Result { get; set; }
    public GetStudentSolutionSheetDto Solution { get; set; }
    public DateTime CreatedUtc { get; set; }
}