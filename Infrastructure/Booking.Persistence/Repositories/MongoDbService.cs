using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Interfaces.Repositories;
using Booking.Application.Interfaces.Repositories.EntityFramework;
using Booking.Application.Interfaces.Repositories.MongoDb;
using Booking.Domain.Models;
using Booking.Domain.Models.MongoDB;
using Booking.Domain.Models.SQLServer;
using Booking.Persistence.MongoDbConfigurations.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Booking.Persistence.Repositories
{
    /// <summary>
    /// Base repository class for handle CRUD ops by using MongoDb
    /// Implements IGenericRepository interface for common functions
    /// </summary>
    /// <typeparam name="TDocument">The type of entity the repository operates on</typeparam>
    public class MongoDbService<TDocument> : IGenericMongoRepository<TDocument> where TDocument : MongoDbBaseEntity, new()
    {
        private readonly IMongoCollection<TDocument> mongoCollection;

        public MongoDbService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            MongoClient client = new MongoClient(mongoDbSettings.Value.DatabaseName);
            IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            mongoCollection = database.GetCollection<TDocument>(nameof(TDocument));
        }

        public async Task<int> AddAsync(TDocument entity)
        {
            await mongoCollection.InsertOneAsync(entity);
            return 1;
        }

        public async Task<int> DeleteAsync(TDocument entity)
        {
            var result = await mongoCollection.DeleteOneAsync(Builders<TDocument>.Filter.Eq(e => e.Id, entity.Id));
            return (int)result.DeletedCount;
        }

        public async Task<TDocument> FirstOrDefaultAsync()
        {
            return await mongoCollection.Find(Builders<TDocument>.Filter.Empty).FirstOrDefaultAsync();
        }

        public async Task<List<TDocument>> GetAllAsync(Expression<Func<TDocument, bool>> filter = null)
        {
            if(filter is not null)
                return await mongoCollection.Find(filter).ToListAsync();

            return await mongoCollection.Find(Builders<TDocument>.Filter.Empty).ToListAsync();
        }

        public async Task<List<TDocument>> GetAllAsync()
        {
            return await mongoCollection.Find(Builders<TDocument>.Filter.Empty).ToListAsync();

        }

        public async Task<TDocument> GetByIdAsync(string id)
        {
            return await mongoCollection.Find(Builders<TDocument>.Filter.Eq(e => e.Id, id)).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateAsync(TDocument entity)
        {
            var result = await mongoCollection.ReplaceOneAsync(Builders<TDocument>.Filter.Eq(e => e.Id, entity.Id), entity);
            return (int)result.ModifiedCount;
        }
    }
}
