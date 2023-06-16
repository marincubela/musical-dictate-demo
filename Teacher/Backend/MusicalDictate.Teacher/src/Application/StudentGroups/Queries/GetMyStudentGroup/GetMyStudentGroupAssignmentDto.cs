using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentGroups.Queries.GetMyStudentGroup;

public class GetMyStudentGroupAssignmentDto : IMapFrom<Assignment>
{
    public string Id { get; set; }
    public string GraderType { get; set; }
    public GetMyStudentGroupExerciseDto Exercise { get; set; }
    public IEnumerable<GetMyStudentGroupStudentSolutionDto> StudentSolutions { get; set; }
}