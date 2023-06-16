using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentSolutions.Queries.GetStudentSolutionsByAssignment;

public class GetStudentSolutionsByAssignmentExerciseDto : IMapFrom<Exercise>
{
    public string Id { get; set; }
    public string Title { get; set; }
    public GetStudentSolutionsByAssignmentSheetDto Solution { get; set; }
    public GetStudentSolutionsByAssignmentTeacherDto Teacher { get; set; }
    public DateTime CreatedUtc { get; set; }
    public IEnumerable<GetStudentSolutionsByAssignmentPartDto> Parts { get; set; }
}