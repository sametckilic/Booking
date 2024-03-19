using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.RequestModels.Hotel
{
    public class DeleteHotelRequestModel
    {
        public DeleteHotelRequestModel(Guid hotel)
        {
            HotelId = hotel;
        }

        public Guid HotelId { get; set; }
    }
}
