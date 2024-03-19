using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Models.SQLServer;
using Booking.Infrastructure.Enums;



namespace Booking.Application.Models.SQLServer
{
    /// <summary>
    /// Represents a 'BillingPayment' object stored in SQL Server
    /// Implementing IEntity interface
    /// </summary>
    public class BillingPayment : IEntity
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string ReservationId { get; set; }
        public Guid HotelId { get; set; }
        public decimal Amount { get; set; }
        public BillingType BillingType { get; set; }
        public DateTime CreateDate { get; set; }


        public virtual Hotel Hotel { get; set; }

    }
}
