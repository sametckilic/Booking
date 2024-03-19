using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Infrastructure.Enums;

namespace Booking.Application.RequestModels.Hotel
{
    public class CreateReservationRequestModel
    {
        public CreateReservationRequestModel(Guid roomId, string userId, BillingType billingType, DateTime checkInDate, DateTime checkOutDate)
        {
            RoomId = roomId;
            UserId = userId;
            BillingType = billingType;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
        }

        public Guid RoomId { get; set; }
        public string UserId { get; set; }
        public BillingType BillingType { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

    }
}
