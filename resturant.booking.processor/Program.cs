using Application.DI;
using resturant.booking.processor;
using resturant.booking.processor.BackgroundServices;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddApplicationService(hostContext.Configuration);

        services.AddHostedService<UserBookingProcessor>();
    })
    .Build();

await host.RunAsync();
