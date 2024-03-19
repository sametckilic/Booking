using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Models.SQLServer;

namespace Booking.Application.Interfaces.Repositories.EntityFramework
{
    /// <summary>
    /// Interaface for a repository handles operations to `HotelRoomPrice` entity
    /// </summary>
    public interface IHotelRoomPriceRepository : IGenericRepository<HotelRoomPrice>
    {
    }
}
