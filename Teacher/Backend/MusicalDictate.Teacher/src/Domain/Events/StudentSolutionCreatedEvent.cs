using Domain.Common;
using Domain.Entities;

namespace Domain.Events;

public class StudentSolutionCreatedEvent : BaseEvent
{
    public StudentSolutionCreatedEvent(StudentSolution solution)
    {
        StudentSolution = solution;
    }

    public StudentSolution StudentSolution { get; set; }

}