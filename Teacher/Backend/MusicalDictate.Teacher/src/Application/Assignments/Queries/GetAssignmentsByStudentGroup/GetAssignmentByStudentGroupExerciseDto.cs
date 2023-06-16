using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Assignments.Queries.GetAssignmentsByStudentGroup;

public class GetAssignmentByStudentGroupExerciseDto : IMapFrom<Exercise>
{
    public string Id { get; set; }
    public string Title { get; set; }
}