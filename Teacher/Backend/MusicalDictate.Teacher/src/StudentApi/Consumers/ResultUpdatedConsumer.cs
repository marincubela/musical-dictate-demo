using Application.StudentSolutions.Queries.GetStudentSolution;
using Domain.Messages;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using StudentApi.Hubs;

namespace StudentApi.Consumers;

public class ResultUpdatedConsumer : IConsumer<ResultUpdated>
{
    readonly ILogger<ResultUpdatedConsumer> _logger;
    private IHubContext<StudentApiHub, IStudentApiHubClient> _hubContext;
    private readonly ISender _sender;

    public ResultUpdatedConsumer(ILogger<ResultUpdatedConsumer> logger, IHubContext<StudentApiHub, IStudentApiHubClient> hubContext, ISender sender)
    {
        _logger = logger;
        _hubContext = hubContext;
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<ResultUpdated> context)
    {
        _logger.LogInformation("Received Message: {Id}", context.Message.StudentSolutionId);
        var solution = await _sender.Send(new GetStudentSolutionQuery(context.Message.StudentSolutionId));
        
        await _hubContext.Clients.All.ResultUpdated(solution.Id, solution.Assignment.Exercise.Title);
    }
}