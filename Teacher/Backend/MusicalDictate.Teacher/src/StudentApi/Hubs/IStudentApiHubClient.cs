namespace StudentApi.Hubs;

public interface IStudentApiHubClient
{
    Task ResultUpdated(string studentSolutionId, string exerciseTitle);
}