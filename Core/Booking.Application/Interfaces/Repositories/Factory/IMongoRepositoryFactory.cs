using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Interfaces.Repositories.MongoDb;

namespace Booking.Application.Interfaces.Repositories.Factory
{
    public interface IMongoRepositoryFactory
    {
        object CreateRepository<T>() where T : class, new();
        IMongoRepository<T> GetRepo<T>() where T : class, new();
    }
}
