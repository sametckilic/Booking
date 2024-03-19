using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.RequestModels.Hotel
{
    public class CreateHotelRequestModel
    {
        public CreateHotelRequestModel(string hotelName)
        {
            HotelName = hotelName;
        }

        public string HotelName { get; set; }
    }
}
