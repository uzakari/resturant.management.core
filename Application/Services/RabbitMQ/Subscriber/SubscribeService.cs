using Application.Repositories.ResturantApp.UserBooking.Interface;
using Application.Services.RabbitMQ.Subscriber.Interface;
using Domain.Models.Request;
using Domain.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Application.Services.RabbitMQ.Subscriber;

public class SubscribeService : ISubscriberServcie
{
    private readonly IConnection _connection;
    private readonly IUserBookingRepository _userBookingRepository;
    private readonly QueueConfiguration _queueConfiguration;
    private readonly IModel _channel;

    public SubscribeService(IConnection connection, IOptions<QueueConfiguration> option, IUserBookingRepository userBookingRepository)
    {
        _connection = connection;
        _userBookingRepository = userBookingRepository;
        _queueConfiguration = option.Value;
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(_queueConfiguration.QueueName, _queueConfiguration.Durable, _queueConfiguration.Exclusive, _queueConfiguration.AutoDelete);

    }
    public Task GetQueueMessageAndProcess()
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += MessageReceived;

        _channel.BasicConsume(_queueConfiguration.QueueName, false, consumer);

        return Task.CompletedTask;
    }

    private async void MessageReceived(object? sender, BasicDeliverEventArgs basicDeliverEvent)
    {
        var body = basicDeliverEvent.Body;
        var message = Encoding.UTF8.GetString(body.ToArray());
        var userBooking = JsonConvert.DeserializeObject<UserBookingDto>(message);
        ArgumentNullException.ThrowIfNull(userBooking);

        var result = await _userBookingRepository.BookAvailableResturant(userBooking);
        if (result != null)
            _channel.BasicAck(basicDeliverEvent.DeliveryTag, false);
    }
}
