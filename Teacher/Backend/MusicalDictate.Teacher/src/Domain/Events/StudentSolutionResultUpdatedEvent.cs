using Domain.Common;
using Domain.Entities;

namespace Domain.Events;

public class StudentSolutionResultUpdatedEvent : BaseEvent
{
    public StudentSolutionResultUpdatedEvent(StudentSolution solution)
    {
        StudentSolution = solution;
    }

    public StudentSolution StudentSolution { get; set; }

}