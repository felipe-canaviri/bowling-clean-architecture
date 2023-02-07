using System.Linq.Expressions;

namespace Bowling.Core.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        ValueTask<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null,
                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                    string includeProperties = "");
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task Update(T entityToUpdate);
        Task UpdateRange(IEnumerable<T> entitiesToUpdate);
    }
}
