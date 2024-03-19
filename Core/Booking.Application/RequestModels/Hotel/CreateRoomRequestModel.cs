using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Infrastructure.Enums;

namespace Booking.Application.RequestModels.Hotel
{
    public class CreateRoomRequestModel
    {
        public CreateRoomRequestModel(Guid hotelId, decimal roomPrice, RoomType roomType)
        {
            HotelId = hotelId;
            RoomPrice = roomPrice;
            RoomType = roomType;
        }

        public Guid HotelId { get; set; }
        public decimal RoomPrice { get; set; }
        public RoomType RoomType { get; set; }

    }
}
