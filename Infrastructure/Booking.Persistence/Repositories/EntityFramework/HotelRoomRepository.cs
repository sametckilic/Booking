using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Interfaces.Repositories.EntityFramework;
using Booking.Application.Models.SQLServer;
using Booking.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Booking.Persistence.Repositories.EntityFramework
{
    /// <summary>
    /// Represents a repository for data ops related 'HotelRoom'
    /// Iplements IHotelRoomRepository interface for ops
    /// Inherits from EFRepositoryBase class for generic Entity Framework data access
    /// </summary>
    public class HotelRoomRepository : EFRepositoryBase<HotelRoom>, IHotelRoomRepository
    {
        public HotelRoomRepository(BookingSQLServerDbContext dbContext) : base(dbContext)
        {
        }
    }
}
