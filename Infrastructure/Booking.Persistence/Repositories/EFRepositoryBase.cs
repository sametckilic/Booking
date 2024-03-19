using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Interfaces.Repositories.EntityFramework;
using Booking.Domain.Models.SQLServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Booking.Persistence.Repositories
{
    /// <summary>
    /// Base repository class for handle CRUD ops by using Entity Framework
    /// Implements IGenericRepository interface for common functions
    /// </summary>
    /// <typeparam name="TEntity">The type of entity the repository operates on</typeparam>
    public class EFRepositoryBase<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity, new()
    {

        private readonly DbContext dbContext;
        protected DbSet<TEntity> entity => dbContext.Set<TEntity>();

        public EFRepositoryBase(DbContext dbContext)
        {
           this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<int> AddAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await this.entity.AddAsync(entity);
            return await dbContext.SaveChangesAsync();

        }
          
        public async Task<int> DeleteAsync(TEntity entity)
        {
           this.entity.Remove(entity);
           return await dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            this.entity.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;

            return await dbContext.SaveChangesAsync();
        }

        public IQueryable<TEntity> AsQueryable() => entity.AsQueryable();

        public async Task<List<TEntity>> GetAll()
        {
            return await entity.ToListAsync();
        }

        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = entity;

            if(predicate != null)
                query = query.Where(predicate);

            foreach(Expression<Func<TEntity, object>> include in includes)
            {
                query = query.Include(include);
            }

            if (orderBy != null)
                query = orderBy(query);

            return await query.ToListAsync();   
        }

        public async Task<TEntity> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes)
        {
            TEntity exist = await entity.FindAsync(id);

            if (exist == null)
                return null;

            foreach(Expression<Func<TEntity, object>> include in includes)
            {
                dbContext.Entry(exist).Reference(include).Load();
            }

            return exist;
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = entity;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = ApplyIncludes(query, includes);

            return await query.SingleOrDefaultAsync();
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return Get(predicate, includes).FirstOrDefaultAsync();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = entity.AsQueryable();

            if(predicate != null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            return query;
        }




        // Applies the specified navigation prop includes to the given queryable.
        private static IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
        {
            if (includes != null)
            {
                foreach (var includeItem in includes)
                {
                    query = query.Include(includeItem);
                }
            }

            return query;
        }
    }
}
