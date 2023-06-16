namespace Application.Common.Interfaces;

public interface IMessageBrokerService
{
    public Task SendMessage<T>(T Message);
}