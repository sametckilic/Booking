using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.RequestModels.Hotel
{
    public class UpdateHotelRequestModel
    {
        public UpdateHotelRequestModel(Guid id, string hotelName)
        {
            Id = id;
            HotelName = hotelName;
        }

        public Guid Id { get; set; }
        public string HotelName { get; set; }
    }
}
