using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Interfaces.Repositories.EntityFramework;
using Booking.Application.Models.MongoDB;

namespace Booking.Application.Interfaces.Repositories.MongoDb
{
    /// <summary>
    /// Interaface for a repository handles operations to `User` entity
    /// </summary>
    public interface IUserRepository : IMongoRepository<User>
    {

    }
}
