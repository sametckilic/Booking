using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.ReservationMailService.Services
{
    /// <summary>
    /// Interface for sending email messages in the application.
    /// </summary>
    public interface IMailService
    {
        // Sends an email asynchronously to provided email
        Task SendEmailAsync(string email, string subject, string message);
    }
}
