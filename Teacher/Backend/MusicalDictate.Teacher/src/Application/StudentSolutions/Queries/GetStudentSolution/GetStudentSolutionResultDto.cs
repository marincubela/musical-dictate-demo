using Application.Common.Mappings;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.StudentSolutions.Queries.GetStudentSolution;

public class GetStudentSolutionResultDto : IMapFrom<Result>
{
    public Grade Grade { get; private set; }
    public GetStudentSolutionTeacherDto Teacher { get; private set; }
    public string Comment { get; private set; }
    public double Percentage { get; private set; }
}