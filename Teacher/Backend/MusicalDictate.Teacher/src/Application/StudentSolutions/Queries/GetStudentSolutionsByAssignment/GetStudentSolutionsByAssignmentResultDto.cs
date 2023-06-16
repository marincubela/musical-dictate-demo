using Application.Common.Mappings;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.StudentSolutions.Queries.GetStudentSolutionsByAssignment;

public class GetStudentSolutionsByAssignmentResultDto : IMapFrom<Result>
{
    public Grade Grade { get; private set; }
    public GetStudentSolutionsByAssignmentTeacherDto Teacher { get; private set; }
    public string Comment { get; private set; }
    public double Percentage { get; private set; }
}