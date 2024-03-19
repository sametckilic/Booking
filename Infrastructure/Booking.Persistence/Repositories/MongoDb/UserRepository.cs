using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Interfaces.Repositories.MongoDb;
using Booking.Application.Models.MongoDB;
using Booking.Persistence.MongoDbConfigurations.Models;
using Microsoft.Extensions.Options;

namespace Booking.Persistence.Repositories.MongoDb
{
    /// <summary>
    /// Represents a repository for data ops related 'User'
    /// Iplements IUserRepository interface for ops
    /// Inherits from MongoDbService class for common MongoDb functions
    /// </summary>
    //public class UserRepository : MongoRepository<User>, IUserRepository
    //{
    //    public UserRepository(string collectionName, MongoDbSettings mongoDbSettings) : base(collectionName, mongoDbSettings)
    //    {
    //    }
    //}
}
