using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Interfaces.Repositories.MongoDb
{
    /// <summary>
    /// Generic MongoDB repository interface for CRUD operations on documents
    /// </summary>
    /// <typeparam name="TDocument">The type of entity that the repository deal</typeparam>
    public interface IMongoRepository<TDocument>
    {
        // returns an TDocument represents queryable collection of document
        IQueryable<TDocument> AsQueryable();

        // filters the documents in the collection by given filter expression
        Task<IEnumerable<TDocument>> FilterBy(Expression<Func<TDocument, bool>> filterExpression);

        // asynchronosuly returns all documents in collection
        Task<List<TDocument>> GetAllAsync();

        // asynchronously insterts a document to collection
        Task<bool> InsertOneAsync(TDocument document);

        // asynchronously finds and returns a single document from collection
        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);

        // asynchronously finds and returns a single document by given id from collection
        Task<TDocument> FindByIdAsync(string id);

        // asynchronously deletes a document from the collection by provided filter expression
        Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression);

        // asynchronously deletes a document from the collection by given id
        Task DeleteByIdAsync(string id);


        // asynchronously replaces a document in the collection with provided document
        Task ReplaceOneAsync(TDocument document);


    }
}
