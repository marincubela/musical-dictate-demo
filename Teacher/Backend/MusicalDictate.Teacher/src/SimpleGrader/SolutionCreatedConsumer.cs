using System.Threading.Tasks;
using Domain.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;
using SimpleGrader.Models;

namespace SimpleGrader;

public class SolutionCreatedConsumer : IConsumer<StudentSolutionCreated>
{
    readonly ILogger<SolutionCreatedConsumer> _logger;

    public SolutionCreatedConsumer(ILogger<SolutionCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<StudentSolutionCreated> context)
    {
        if (context.Message.GraderType != "simple")
            return;

        _logger.LogInformation("Received Message: {Id}, {Type}", context.Message.StudentSolutionId, context.Message.GraderType);

        await FlurlClient.UpdateStudentSolutionResult(new UpdateResultDto {StudentSolutionId = context.Message.StudentSolutionId, Comment = "Ovo je strojni pregled", Percentage = 86, Grade = 4});
    }
}