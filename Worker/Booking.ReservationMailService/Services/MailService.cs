using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Interfaces.Repositories.Factory;
using Booking.Application.Interfaces.Repositories.MongoDb;
using Booking.Application.Models.MongoDB;

namespace Booking.ReservationMailService.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = _configuration["SmtpConfig:User"];
            var password = _configuration["SmtpConfig:Password"];


            var client = new SmtpClient(_configuration["SmtpConfig:Host"], Convert.ToInt32(_configuration["SmtpConfig:Port"]))
            {
                EnableSsl = Convert.ToBoolean(_configuration["SmtpConfig:UseSSL"]),
                Credentials = new NetworkCredential(mail, password)
            };

            return client.SendMailAsync(new MailMessage(from: new MailAddress(mail).Address,
                                                        to: email,
                                                        subject,
                                                        message));
                
        }
    }
}
