using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.RequestModels.Hotel
{
    public class IsRoomAvaliableRequestModel
    {
        public IsRoomAvaliableRequestModel(Guid roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            RoomId = roomId;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
        }

        public Guid RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
