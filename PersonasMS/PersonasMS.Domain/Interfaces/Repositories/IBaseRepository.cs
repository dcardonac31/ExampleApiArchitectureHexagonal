using System.Linq.Expressions;

namespace PersonasMS.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(int page, int limit, string orderBy, bool ascending = true);
        Task<IQueryable<TEntity>> AsQueryable();
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties);
        bool Insert(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
    }
}
