using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Models.SQLServer;

namespace Booking.Application.Interfaces.Repositories.EntityFramework
{
    /// <summary>
    /// Generic repository interface for CRUD operations on entities
    /// </summary>
    /// <typeparam name="T">The type of entity that the repository deal</typeparam>
    public interface IGenericRepository<T> where T : class, IEntity, new()
    {
        // asynchronously adds a new entity to the database
        Task<int> AddAsync(T entity);

        // asynchronously updates a exist entity in the database
        Task<int> UpdateAsync(T entity);

        // asynchronously deletes a exist entity in the database 
        Task<int> DeleteAsync(T entity);


        // Returns an IQueryable<T> representing a queryable collection of entities.
        IQueryable<T> AsQueryable();

        // Returns all entities from the database.
        Task<List<T>> GetAll();

        // Retruns a filtered and ordered list of entities based on the specified predicate and optional parameters.
        Task<List<T>> GetList(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes);

        // Returns an entity by its unique identifier asynchronously.
        Task<T> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes);

        // Returns a single entity based on the specified predicate and optional navigation properties to include.
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        // asynchronously returns the first entity in database or returns default value if not found
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        // Returns a queryable collection of entities based on the specified predicate and optional parameters.
        IQueryable<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);


    }
}
