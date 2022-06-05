using Application.DI.Behaviour;
using Application.Repositories.ResturantApp.Base;
using Application.Repositories.ResturantApp.Base.Interface;
using Application.Repositories.ResturantApp.Resturant;
using Application.Repositories.ResturantApp.Resturant.Interface;
using Application.Repositories.ResturantApp.ResturantOwner;
using Application.Repositories.ResturantApp.ResturantOwner.Interface;
using Domain.DB;
using Domain.Settings;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Domain.Models.Request;
using Application.DI.RabbitMQ;
using Application.Services.RabbitMQ.Publisher;
using Application.Services.RabbitMQ.Subscriber.Interface;
using Application.Services.RabbitMQ.Subscriber;
using Application.Repositories.ResturantApp.UserBooking;
using Application.Repositories.ResturantApp.UserBooking.Interface;
using Application.Services.RabbitMQ.Publisher.Interface;
using Application.DI.Authentication;

namespace Application.DI;

public static class ApplicationService
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddBehaviourService();
        services.Configure<QueueConfiguration>(configuration.GetSection("QueueSettings"));
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddMessageBroker<UserBookingDto>(configuration);
        services.AddCustomAuthentication(configuration);
        services.AddDbContext<ResturantDBContext>(opt => opt.UseSqlite(configuration.GetConnectionString("ResturantConnection")));


        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IResturantRepository, ResturantRepository>();
        services.AddTransient<IResturantOwnerRepository, ResturantOwnerRepository>();
        services.AddScoped<IUserBookingRepository, UserBookingRepository>();
        services.AddScoped<IPublishService, PublishService>();
        services.AddScoped<ISubscriberServcie, SubscribeService>();
        return services;
    }

}
