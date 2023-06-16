using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentSolutions.Queries.GetStudentSolution;

public class GetStudentSolutionPartDto : IMapFrom<Part>
{
    public int Start { get; set; }
    public int End { get; set; }
    public int AllowedTimeInSeconds { get; set; }
    public int AllowedNumberOfAttempts { get; set; }
}