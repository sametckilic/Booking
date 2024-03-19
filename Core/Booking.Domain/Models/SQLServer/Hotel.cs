using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Models.SQLServer;

namespace Booking.Application.Models.SQLServer
{
    /// <summary>
    /// Represents a 'Hotel' object stored in SQL Server
    /// Implementing IEntity interface
    /// </summary>
    public class Hotel : IEntity
    {
        public Guid Id { get; set; }
        public string HotelName { get; set; }


        
        public virtual ICollection<BillingPayment> BillingPayments { get; set; }
        public virtual ICollection<HotelRoom> HotelRooms { get; set; }
        public virtual ICollection<HotelRoomPrice> HotelRoomPrices { get; set; }
    }
}
