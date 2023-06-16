using System.Threading.Tasks;
using Domain.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;
using SimpleGrader.Interfaces;
using SimpleGrader.Models;
using SimpleGrader.Services;

namespace SimpleGrader.Consumers;

public class SolutionCreatedConsumer : IConsumer<StudentSolutionCreated>
{
    private readonly ILogger<SolutionCreatedConsumer> _logger;
    private readonly IWebApiClient _webApiClient;

    public SolutionCreatedConsumer(ILogger<SolutionCreatedConsumer> logger, IWebApiClient webApiClient)
    {
        _logger = logger;
        _webApiClient = webApiClient;
    }

    public async Task Consume(ConsumeContext<StudentSolutionCreated> context)
    {
        _logger.LogInformation("Received Message: {Id}, {Type}", context.Message.StudentSolutionId, context.Message.GraderType);

        if (context.Message.GraderType != "simple")
            return;

        await _webApiClient.UpdateStudentSolutionResult(new UpdateResultDto {StudentSolutionId = context.Message.StudentSolutionId, Comment = "Ovo je strojni pregled", Percentage = 86, Grade = 4});
    }
}