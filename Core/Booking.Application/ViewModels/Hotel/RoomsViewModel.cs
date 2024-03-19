using Booking.Infrastructure.Enums;

namespace Booking.Application.ViewModels.Hotel
{
    public class RoomsViewModel
    {
        public Guid RoomId { get; set; }
        public decimal RoomPrice { get; set; }
        public RoomType RoomType { get; set; }
    }
}
