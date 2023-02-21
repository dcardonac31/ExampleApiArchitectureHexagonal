using Microsoft.EntityFrameworkCore;
using PersonasMS.Infraestructure.Data.DatabaseContext;

namespace PersonasMS.Infraestructure.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PersonasMsDbContext _dbContext;
        private bool _disposed = false;

        public UnitOfWork(PersonasMsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UnitOfWork()
        {

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int Save() => _dbContext.SaveChanges();

        public void Rollback() => _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());

    }
}
