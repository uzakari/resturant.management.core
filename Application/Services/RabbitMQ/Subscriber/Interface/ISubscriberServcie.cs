namespace Application.Services.RabbitMQ.Subscriber.Interface;

public interface ISubscriberServcie
{
    Task GetQueueMessageAndProcess();
}
