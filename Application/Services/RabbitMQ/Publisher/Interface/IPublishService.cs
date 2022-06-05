namespace Application.Services.RabbitMQ.Publisher.Interface;

public interface IPublishService
{
    Task SendMessage<T>(T message) where T : class;
}
