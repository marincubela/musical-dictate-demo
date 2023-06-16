using Application.Common.Interfaces;
using Domain.Events;
using Domain.Messages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.StudentSolutions.EventHandlers;

public class StudentSolutionCreatedEventHandler : INotificationHandler<StudentSolutionCreatedEvent>
{
    private readonly ILogger<StudentSolutionCreatedEventHandler> _logger;
    private readonly IMessageBrokerService _messageBrokerService;

    public StudentSolutionCreatedEventHandler(ILogger<StudentSolutionCreatedEventHandler> logger, IMessageBrokerService messageBrokerService)
    {
        _logger = logger;
        _messageBrokerService = messageBrokerService;
    }

    public async Task Handle(StudentSolutionCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);
        await _messageBrokerService.SendMessage(new StudentSolutionCreated() {StudentSolutionId = notification.StudentSolution.Id, GraderType = notification.StudentSolution.Assignment.GraderType});
    }
}