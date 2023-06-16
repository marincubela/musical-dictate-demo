using Application.Common.Mappings;
using Application.StudentSolutions.Queries.GetStudentSolution;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.StudentSolutions.Queries.GetMyStudentSolutions;

public class GetMyStudentSolutionsResultDto : IMapFrom<Result>
{
    public Grade Grade { get; private set; }
    public GetStudentSolutionTeacherDto Teacher { get; private set; }
    public string Comment { get; private set; }
    public double Percentage { get; private set; }
}