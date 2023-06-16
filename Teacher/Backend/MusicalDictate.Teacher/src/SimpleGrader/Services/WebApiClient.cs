using System;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using SimpleGrader.Interfaces;
using SimpleGrader.Models;

namespace SimpleGrader.Services;

public class WebApiClient : IWebApiClient
{
    private readonly IConfiguration _configuration;

    public WebApiClient(IConfiguration configuration) {
        _configuration = configuration;
    }

    private Token? _token;

    public async Task LoginGrader(string email, string password)
    {
        var response = await new Url($"{_configuration["Endpoints:WebApi"]}/api/auth/login/grader")
            .PostJsonAsync(new {email, password})
            .ReceiveJson<Token>();

        _token = response;
    }

    public async Task RefreshToken()
    {
        if (_token == null)
            throw new ArgumentNullException($"Token", "Can't refresh token if it's null");

        var response = await new Url($"{_configuration["Endpoints:WebApi"]}/api/auth/refresh-token")
            .PostJsonAsync(new {accessToken = _token.AccessToken, refreshToken = _token.RefreshToken})
            .ReceiveJson<Token>();

        _token = response;
    }

    private async Task Refresh()
    {
        if (_token == null)
            await LoginGrader("grader@mail.com", "Grader123!");

        await RefreshToken();
    }

    public async Task<StudentSolution> GetStudentSolution(string studentSolutionId)
    {
        var response = await new Url($"{_configuration["Endpoints:WebApi"]}/api/StudentSolutions/{studentSolutionId}")
            .AllowHttpStatus("401")
            .WithOAuthBearerToken(_token?.AccessToken)
            .GetAsync();

        if (response.StatusCode != 401)
            return await response.GetJsonAsync<StudentSolution>();

        await Refresh();
        return await new Url($"{_configuration["Endpoints:WebApi"]}/api/StudentSolutions/{studentSolutionId}")
            .WithOAuthBearerToken(_token?.AccessToken)
            .GetJsonAsync<StudentSolution>();
    }

    public async Task UpdateStudentSolutionResult(UpdateResultDto dto)
    {
        var response = await new Url($"{_configuration["Endpoints:WebApi"]}/api/StudentSolutions/result")
            .AllowHttpStatus("401")
            .WithOAuthBearerToken(_token?.AccessToken)
            .PutJsonAsync(dto);

        if (response.StatusCode != 401)
            return;

        await Refresh();
        await new Url($"{_configuration["Endpoints:WebApi"]}/api/StudentSolutions/result")
            .WithOAuthBearerToken(_token?.AccessToken)
            .PutJsonAsync(dto);
    }
}