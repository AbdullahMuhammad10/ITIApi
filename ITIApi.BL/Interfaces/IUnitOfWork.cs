namespace ITIApi.BL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IRepository<T> Repository<T>() where T : class;
        int Complete();
    }
}
