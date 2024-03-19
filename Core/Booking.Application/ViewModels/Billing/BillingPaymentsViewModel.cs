using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Infrastructure.Enums;

namespace Booking.Application.ViewModels.Billing
{
    public class BillingPaymentsViewModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string ReservationId { get; set; }
        public Guid HotelId { get; set; }
        public decimal Amount { get; set; }
        public BillingType BillingType { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
