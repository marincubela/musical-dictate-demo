using System;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using SimpleGrader.Models;

namespace SimpleGrader;

public class FlurlClient
{
    private static Token? _token;

    private static string RootPath = "https://localhost:7157/";

    public static async Task LoginGrader(string email, string password)
    {
        var response = await new Url(RootPath + $"api/auth/login/grader")
            .PostJsonAsync(new {email, password})
            .ReceiveJson<Token>();

        _token = response;
    }

    public static async Task RefreshToken()
    {
        if (_token == null)
            throw new ArgumentNullException($"Token", "Can't refresh token if it's null");

        var response = await new Url(RootPath + $"api/auth/refresh-token")
            .PostJsonAsync(new {accessToken = _token.AccessToken, refreshToken = _token.RefreshToken})
            .ReceiveJson<Token>();

        _token = response;
    }

    public async static Task Refresh()
    {
        if (_token == null)
            await LoginGrader("grader@mail.com", "Grader123!");

        await RefreshToken();
    }

    public static async Task<StudentSolution> GetStudentSolution(string studentSolutionId)
    {
        var response = await new Url(RootPath + $"api/StudentSolutions/{studentSolutionId}")
            .AllowHttpStatus("401")
            .WithOAuthBearerToken(_token?.AccessToken)
            .GetAsync();

        if (response.StatusCode != 401)
            return await response.GetJsonAsync<StudentSolution>();

        await Refresh();
        return await new Url(RootPath + $"api/StudentSolutions/{studentSolutionId}")
            .WithOAuthBearerToken(_token?.AccessToken)
            .GetJsonAsync<StudentSolution>();
    }

    public static async Task UpdateStudentSolutionResult(UpdateResultDto dto)
    {
        var response = await new Url(RootPath + $"api/StudentSolutions/result")
            .AllowHttpStatus("401")
            .WithOAuthBearerToken(_token?.AccessToken)
            .PutJsonAsync(dto);

        if (response.StatusCode != 401)
            return;

        await Refresh();
        await new Url(RootPath + $"api/StudentSolutions/result")
            .WithOAuthBearerToken(_token?.AccessToken)
            .PutJsonAsync(dto);
    }
}