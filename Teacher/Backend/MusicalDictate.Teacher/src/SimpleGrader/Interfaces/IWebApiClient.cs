using System.Threading.Tasks;
using SimpleGrader.Models;

namespace SimpleGrader.Interfaces;

public interface IWebApiClient
{
    public Task LoginGrader(string email, string password);

    public Task RefreshToken();

    public Task<StudentSolution> GetStudentSolution(string studentSolutionId);

    public Task UpdateStudentSolutionResult(UpdateResultDto dto);
}