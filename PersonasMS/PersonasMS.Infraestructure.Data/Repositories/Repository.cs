using Microsoft.EntityFrameworkCore;
using PersonasMS.Domain.Interfaces.Repositories;
using PersonasMS.Infraestructure.Data.DatabaseContext;
using PersonasMS.Infraestructure.Data.Pagination;
using PersonasMS.Infraestructure.Data.UnitOfWork;
using System.Linq.Expressions;

namespace PersonasMS.Infraestructure.Data.Repositories
{
    public class Repository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly PersonasMsDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<TEntity> _entities;

        public Repository(PersonasMsDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _entities = dbContext.Set<TEntity>();
            _unitOfWork = unitOfWork;
        }

        public bool Insert(TEntity entity)
        {
            _entities.Add(entity);
            var result = _unitOfWork.Save();
            return result > 0;
        }

        public bool Update(TEntity entity)
        {
            _entities.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return _unitOfWork.Save() > 0;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(int page, int limit, string orderBy, bool ascending = true)
        {
            var result = await PagedResult<TEntity>.ToPagedListAsync(_entities.AsQueryable(), page, limit, orderBy, ascending);

            return result;
        }

        public async Task<IQueryable<TEntity>> AsQueryable()
        {
            return _entities.AsQueryable();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = await AsQueryable();
            query = PerformInclusions(includeProperties, query);
            return query.FirstOrDefault(where);
        }

        public bool Delete(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _entities.Attach(entity);
            }
            _entities.Remove(entity);
            return _dbContext.SaveChanges() > 0;
            //return _unitOfWork.Save() > 0;
        }

        private IQueryable<TEntity> PerformInclusions(IEnumerable<Expression<Func<TEntity, object>>> includeProperties, IQueryable<TEntity> query)
        {
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }



    }
}
