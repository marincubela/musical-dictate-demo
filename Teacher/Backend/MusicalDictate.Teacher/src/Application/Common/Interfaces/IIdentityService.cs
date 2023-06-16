using Application.Common.Models;

namespace Application.Common.Interfaces;

public interface IIdentityService
{
    public Task<string> GetUserNameAsync(string id);
    public Task<Token> CreateStudent(string email, string jmbag, string firstName, string lastName, string nameClass, string password, CancellationToken cancellationToken);
    public Task<Token> CreateTeacher(string email, string firstName, string lastName, string password, CancellationToken cancellationToken);
    public Task<Token?> LoginStudent(string email, string password);
    public Task<Token?> LoginTeacher(string email, string password);
    public Task<Token?> LoginGrader(string email, string password);
    public Task<Token> RefreshToken(Token token);
    public Task Revoke(string id);
    public Task LogoutUser();
}