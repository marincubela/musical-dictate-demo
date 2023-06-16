namespace TeacherApi.Hubs;

public interface ITeacherApiHubClient
{
    Task StudentSolutionCreated(string firstName, string lastName);
}