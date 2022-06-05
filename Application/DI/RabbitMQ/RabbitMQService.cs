using Application.Commands.UserBookings;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Application.DI.RabbitMQ;

public static class RabbitMQService
{
    public static IServiceCollection AddMessageBroker<TObject>(this IServiceCollection services, IConfiguration configuration) where TObject : class 
    {
        var connectionFactory = new ConnectionFactory()
        {
            Password = ConnectionFactory.DefaultPass,
            UserName = ConnectionFactory.DefaultUser,
            HostName = configuration.GetConnectionString("RabbitMQ")
        };
        services.AddSingleton<IConnection>(connectionFactory.CreateConnection());
        services.AddScoped(typeof(IRequestHandler<UserBookingsCommand<TObject>, Unit>), typeof(UserBookingsCommandHandler<TObject>));
        return services;
    }
}
