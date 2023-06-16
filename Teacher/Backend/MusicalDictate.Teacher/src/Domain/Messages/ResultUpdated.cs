namespace Domain.Messages;

public record ResultUpdated
{
    public string StudentSolutionId { get; init; }
}