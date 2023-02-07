using Bowling.Core.Interfaces.Repositories;
using Bowling.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bowling.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        internal AppDbContext Context;
        internal DbSet<T> dbSet;

        public BaseRepository(AppDbContext context)
        {
            Context = context;
            dbSet = context.Set<T>();
        }

        public virtual async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await dbSet.AddRangeAsync(entities);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, 
                                                           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
                                                           string includeProperties = "")
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy == null)
            {
                return await query.ToListAsync();
            }
            
            return await orderBy(query).ToListAsync();
        }

        public virtual async  ValueTask<T> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.SingleOrDefaultAsync(predicate);
        }

        public virtual async Task Update(T entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual async Task UpdateRange(IEnumerable<T> entitiesToUpdate)
        {
            dbSet.AttachRange(entitiesToUpdate);
            Context.Entry(entitiesToUpdate).State = EntityState.Modified;
        }
    }
}
