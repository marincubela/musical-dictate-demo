namespace Domain.Messages;

public record StudentSolutionCreated
{
    public string StudentSolutionId { get; init; }
    public string GraderType { get; init; }
}