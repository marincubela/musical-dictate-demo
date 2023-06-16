using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentSolutions.Queries.GetStudentSolution;

public class GetStudentSolutionExerciseDto : IMapFrom<Exercise>
{
    public string Id { get; set; }
    public string Title { get; set; }
    public GetStudentSolutionSheetDto Solution { get; set; }
    public GetStudentSolutionTeacherDto Teacher { get; set; }
    public DateTime CreatedUtc { get; set; }
    public IEnumerable<GetStudentSolutionPartDto> Parts { get; set; }
}