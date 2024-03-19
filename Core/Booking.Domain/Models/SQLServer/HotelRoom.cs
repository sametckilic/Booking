using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Models.SQLServer;
using Booking.Infrastructure.Enums;

namespace Booking.Application.Models.SQLServer
{
    /// <summary>
    /// Represents a 'HotelRoom' object stored in SQL Server
    /// Implementing IEntity interface
    /// </summary>
    public class HotelRoom : IEntity
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public RoomType RoomType { get; set; }


        public virtual Hotel Hotel { get; set; }
        public virtual ICollection<HotelRoomPrice> HotelRoomPrices { get; set; }
    }
}
