using Microsoft.AspNetCore.SignalR;

namespace StudentApi.Hubs;

public class StudentApiHub : Hub<IStudentApiHubClient>
{
    private readonly ILogger<StudentApiHub> _logger;

    public StudentApiHub(ILogger<StudentApiHub> logger)
    {
        _logger = logger;
    }

    public async Task ResultUpdated(string studentSolutionId, string exerciseTitle)
    {
        _logger.LogInformation("Sending: {StudentSolutionId}, {ExerciseTitle}", studentSolutionId, exerciseTitle);
        await Clients.All.ResultUpdated(studentSolutionId, exerciseTitle);
    }
}