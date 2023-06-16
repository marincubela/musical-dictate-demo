using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Assignments.Queries.GetAssignment;

public class GetAssignmentPartDto : IMapFrom<Part>
{
    public int Start { get; set; }
    public int End { get; set; }
    public int AllowedTimeInSeconds { get; set; }
    public int AllowedNumberOfAttempts { get; set; }
}