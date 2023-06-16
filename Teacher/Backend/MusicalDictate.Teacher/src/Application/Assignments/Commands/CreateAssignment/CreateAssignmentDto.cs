using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Assignments.Commands.CreateAssignment;

public class CreateAssignmentDto : IMapFrom<Assignment>
{
    public string Id { get; set; }
    public string Title { get; set; }
}