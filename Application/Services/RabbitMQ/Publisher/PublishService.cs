using Application.Services.RabbitMQ.Publisher.Interface;
using Domain.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Application.Services.RabbitMQ.Publisher;

public class PublishService : IPublishService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly QueueConfiguration _queueConfiguration;

    public PublishService(IConnection connection, IOptions<QueueConfiguration> option)
    {
        _connection = connection;
        _channel = _connection.CreateModel();
        _queueConfiguration = option.Value;

        _channel.QueueDeclare(_queueConfiguration.QueueName, _queueConfiguration.Durable, _queueConfiguration.Exclusive, _queueConfiguration.AutoDelete);
    }
    public Task SendMessage<T>(T message) where T : class
    {
        _channel.BasicPublish("", _queueConfiguration.QueueName, null, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
        return Task.CompletedTask;
    }
}
