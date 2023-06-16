using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentGroups.Queries.GetStudentGroup;

public class GetStudentGroupAssignmentDto : IMapFrom<Assignment>
{
    public string Id { get; set; }
    public string GraderType { get; set; }
    public GetStudentGroupExerciseDto Exercise { get; set; }
}