using Application.Common.Interfaces;
using MassTransit;

namespace Infrastructure.Services;

public class MessageBrokerService : IMessageBrokerService
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MessageBrokerService(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task SendMessage<T>(T message)
    {
        await _publishEndpoint.Publish(message);
    }
}