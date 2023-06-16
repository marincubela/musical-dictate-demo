using Application.StudentSolutions.Queries.GetStudentSolution;
using Domain.Messages;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using TeacherApi.Hubs;

namespace TeacherApi.Consumers;

public class StudentSolutionCreatedConsumer : IConsumer<StudentSolutionCreated>
{
    readonly ILogger<StudentSolutionCreatedConsumer> _logger;
    private IHubContext<TeacherApiHub, ITeacherApiHubClient> _hubContext;
    private readonly ISender _sender;

    public StudentSolutionCreatedConsumer(ILogger<StudentSolutionCreatedConsumer> logger, IHubContext<TeacherApiHub, ITeacherApiHubClient> hubContext, ISender sender)
    {
        _logger = logger;
        _hubContext = hubContext;
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<StudentSolutionCreated> context)
    {
        _logger.LogInformation("Received Message: {Id}, {Type}", context.Message.StudentSolutionId, context.Message.GraderType);
        var solution = await _sender.Send(new GetStudentSolutionQuery(context.Message.StudentSolutionId));
        
        await _hubContext.Clients.All.StudentSolutionCreated(solution.Student.FirstName, solution.Student.LastName);
    }
}