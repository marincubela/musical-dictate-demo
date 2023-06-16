using Application.Common.Interfaces;
using Domain.Events;
using Domain.Messages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.StudentSolutions.EventHandlers;

public class StudentSolutionResultUpdatedEventHandler : INotificationHandler<StudentSolutionResultUpdatedEvent>
{
    private readonly ILogger<StudentSolutionResultUpdatedEventHandler> _logger;
    private readonly IMessageBrokerService _messageBrokerService;

    public StudentSolutionResultUpdatedEventHandler(ILogger<StudentSolutionResultUpdatedEventHandler> logger, IMessageBrokerService messageBrokerService)
    {
        _logger = logger;
        _messageBrokerService = messageBrokerService;
    }

    public async Task Handle(StudentSolutionResultUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);
        await _messageBrokerService.SendMessage(new ResultUpdated() {StudentSolutionId = notification.StudentSolution.Id});
    }
}