using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Interfaces.Repositories.EntityFramework;
using Booking.Application.Interfaces.Repositories.MongoDb;
using Booking.Domain.Models.MongoDB;
using Booking.Domain.Models.SQLServer;
using Booking.Persistence.MongoDbConfigurations.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Booking.Persistence.Repositories
{
    /// <summary>
    /// Base repository class for handle CRUD ops by using MongoDb
    /// Implements IGenericRepository interface for common functions
    /// </summary>
    /// <typeparam name="TDocument">The type of entity the repository operates on</typeparam>
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : Document, new()
    {
        private readonly IMongoCollection<TDocument> mongoCollection;

        public MongoRepository(string collectionName, MongoDbSettings mongoDbSettings)
        {
            MongoClient client = new MongoClient(mongoDbSettings.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDbSettings.DatabaseName);
            mongoCollection = database.GetCollection<TDocument>(collectionName);
        }

        public IQueryable<TDocument> AsQueryable()
        {
            return mongoCollection.AsQueryable();
        }

        public Task DeleteByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<TDocument>.Filter.Eq(doc => new ObjectId(doc.Id), objectId);
                mongoCollection.FindOneAndDeleteAsync(filter);
            });

        }

        public async Task<List<TDocument>> GetAllAsync()
        {
            var result = await mongoCollection.Find(_  => true).ToListAsync();

            return result;
        }

        public Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => mongoCollection.FindOneAndDeleteAsync(filterExpression));
        }

        public async Task<IEnumerable<TDocument>> FilterBy(Expression<Func<TDocument, bool>> predicate)
        {
            var result = await mongoCollection.FindAsync(predicate);
            return result.ToList();
        }

        public Task<TDocument> FindByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
                return mongoCollection.Find(filter).SingleOrDefaultAsync();
            });
        }

        public Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => mongoCollection.Find(filterExpression).FirstOrDefaultAsync());
        }

        public async Task<bool> InsertOneAsync(TDocument document)
        {
            await mongoCollection.InsertOneAsync(document);
            return true;
        }

        public async Task ReplaceOneAsync(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            await mongoCollection.FindOneAndReplaceAsync(filter, document);
        }
    }
}
