using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Models.SQLServer;

namespace Booking.Application.Interfaces.Repositories.EntityFramework
{
    /// <summary>
    /// Interaface for a repository handles operations to `Hotel` entity
    /// </summary>
    public interface IHotelRepository : IGenericRepository<Hotel>
    {
    }
}
