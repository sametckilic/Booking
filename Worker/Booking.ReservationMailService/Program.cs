using Booking.Application.Interfaces.Queue;
using Booking.Persistence.MongoDbConfigurations.Models;
using Booking.Persistence.Queue;
using Booking.ReservationMailService;
using Booking.ReservationMailService.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<IRabbitmqFactory, RabbitmqFactory>();
        services.AddSingleton<IMailService, MailService>();
    })
    .Build();

host.Run();
