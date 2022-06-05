using Application.Services.RabbitMQ.Subscriber.Interface;

namespace resturant.booking.processor.BackgroundServices;

public class UserBookingProcessor : BackgroundService
{
    private readonly ILogger<UserBookingProcessor> _logger;
    private readonly IServiceProvider _serviceProvider;

    public UserBookingProcessor(ILogger<UserBookingProcessor> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            var scope = _serviceProvider.CreateScope();
            var subscriberServcie = scope.ServiceProvider.GetRequiredService<ISubscriberServcie>();
            await subscriberServcie.GetQueueMessageAndProcess();
            await Task.Delay(1000, stoppingToken);
        }
    }
}
