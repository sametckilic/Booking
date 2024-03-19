using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Models;
using Booking.Domain.Models.MongoDB;
using Booking.Domain.Models.SQLServer;

namespace Booking.Application.Interfaces.Repositories.MongoDb
{
    /// <summary>
    /// Generic MongoDB repository interface for CRUD operations on documents
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    public interface IGenericMongoRepository<TDocument> where TDocument : MongoDbBaseEntity
    {
        // asynchronously adds a new entity to the database
        Task<int> AddAsync(TDocument document);

        // asynchronously updates a exist entity in the database
        Task<int> UpdateAsync(TDocument document);

        // asynchronously deletes a exist entity in the database 
        Task<int> DeleteAsync(TDocument document);
        
        // asynchronously returns the first entity in database or returns default value if not found
        Task<TDocument> FirstOrDefaultAsync();

        // Returns all entities from the database by giving filter
        Task<List<TDocument>> GetAllAsync(Expression<Func<TDocument, bool>> filter = null);

        // Returns all entities from the database
        Task<List<TDocument>> GetAllAsync();

        // Returns an entity by its unique identifier asynchronously.
        Task<TDocument> GetByIdAsync(string id);


    }
}
