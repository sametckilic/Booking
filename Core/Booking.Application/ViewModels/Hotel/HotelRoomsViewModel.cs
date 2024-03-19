using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Models.SQLServer;

namespace Booking.Application.ViewModels.Hotel
{
    /// <summary>
    /// A viewmodel represents Rooms list
    /// </summary>
    public class HotelRoomsViewModel
    {
        public Guid HotelId { get; set; }
        public string HotelName { get; set; }
        public IEnumerable<RoomsViewModel> Rooms { get; set; }
    }
}
