using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.RequestModels.Reservation
{
    public class UpdateReservationRequest
    {

        public string Id { get; set; }
        public Guid HotelId {  get; set; }
        public Guid RoomId { get; set; }
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

    }
}
