using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Interfaces.Repositories.EntityFramework;
using Booking.Domain.Models.SQLServer;
using Booking.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Booking.Persistence.Repositories.EntityFramework
{
    /// <summary>
    /// Represents a repository for data ops related 'HotelRoomPrice'
    /// Iplements IHotelRoomPriceRepository interface for ops
    /// Inherits from EFRepositoryBase class for generic Entity Framework data access
    /// </summary>
    public class HotelRoomPriceRepository : EFRepositoryBase<HotelRoomPrice>, IHotelRoomPriceRepository
    {
        public HotelRoomPriceRepository(BookingSQLServerDbContext dbContext) : base(dbContext)
        {
        }
    }
}
