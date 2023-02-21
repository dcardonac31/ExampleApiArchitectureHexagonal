namespace PersonasMS.Infraestructure.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        new void Dispose();
        int Save();
        void Rollback();
    }
}
