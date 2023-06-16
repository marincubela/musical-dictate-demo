using Microsoft.AspNetCore.SignalR;

namespace TeacherApi.Hubs;

public class TeacherApiHub : Hub<ITeacherApiHubClient>
{
    private readonly ILogger<TeacherApiHub> _logger;

    public TeacherApiHub(ILogger<TeacherApiHub> logger)
    {
        _logger = logger;
    }

    public async Task StudentSolutionCreated(string firstName, string lastName)
    {
        _logger.LogInformation("Sending: {FirstName}, {LastName}", firstName, lastName);
        await Clients.All.StudentSolutionCreated(firstName, lastName);
    }
}