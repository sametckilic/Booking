using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Models.SQLServer;

namespace Booking.Application.Interfaces.Repositories.EntityFramework
{
    /// <summary>
    /// Interaface for a repository handles operations to `HotelRoom` entity
    /// </summary>
    public interface IHotelRoomRepository : IGenericRepository<HotelRoom>
    {
    }
}
