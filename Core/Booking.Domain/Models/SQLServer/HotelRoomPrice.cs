using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Models.SQLServer;
using Booking.Infrastructure.Enums;

namespace Booking.Domain.Models.SQLServer
{
    /// <summary>
    /// Represents a 'HotelRoomPrice' object stored in SQL Server
    /// Implementing IEntity interface
    /// </summary>
    public class HotelRoomPrice : IEntity
    {
        public Guid Id { get; set; }
        public Guid HotelRoomId { get; set; }
        public Guid HotelId { get; set; }
        public RoomType RoomType { get; set; }
        public decimal Price { get; set; }


        public virtual Hotel Hotel { get; set; }
        public virtual HotelRoom HotelRoom {  get; set; } 
    }
}
