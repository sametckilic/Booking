
using Booking.Application.Interfaces.Queue;
using Booking.Infrastructure.Events;
using Booking.ReservationMailService.Services;

namespace Booking.ReservationMailService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IRabbitmqFactory rabbitmqFactory;
        private readonly IMailService mailService;

        public Worker(ILogger<Worker> logger, IRabbitmqFactory rabbitmqFactory, IMailService mailService)
        {
            _logger = logger;
            this.rabbitmqFactory = rabbitmqFactory;
            this.mailService = mailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            rabbitmqFactory.Receive<ReservationCreatedEvent>(async reservation =>
            {
                string email = reservation.Email;
                string subject = "About your reservation";
                string message = $"Hello {reservation.FirstName} {reservation.LastName} \n " +
                $"Your reservation has created successfully between {reservation.CheckInDate.ToString("dd/MM/yyyy")} " +
                $"to {reservation.CheckOutDate.ToString("dd/MM/yyyy")} \n" +
                $"Your room id is {reservation.RoomId}";

                try
                {
                    await mailService.SendEmailAsync(email, subject, message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }


            }, 
            queueName: "ReservationQueue", 
            exchangeName: "Reservation");

            Console.ReadLine();
        }
    }
}
