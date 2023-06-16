using Domain.Common;
using Domain.Entities;

namespace Domain.Events;

public class ExerciseCreatedEvent : BaseEvent
{
    public ExerciseCreatedEvent(Exercise exercise)
    {
        Exercise = exercise;
    }

    public Exercise Exercise { get; }
}