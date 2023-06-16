using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentSolutions.Queries.GetStudentSolutionsByAssignment;

public class GetStudentSolutionsByAssignmentDto : IMapFrom<StudentSolution>
{
    public string Id { get; set; }
    public GetStudentSolutionsByAssignmentStudentDto Student { get; set; }
    public GetStudentSolutionsByAssignmentAssignmentDto Assignment { get; set; }
    public GetStudentSolutionsByAssignmentResultDto Result { get; set; }
    public GetStudentSolutionsByAssignmentSheetDto Solution { get; set; }
    public DateTime CreatedUtc { get; set; }
}