using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Assignments.Queries.GetAssignment;

public class GetAssignmentDto : IMapFrom<Assignment>
{
    public string Id { get; set; }
    public string GraderType { get; set; }
    public GetAssignmentExerciseDto Exercise { get; set; }
}