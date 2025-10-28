using ITIApi.BL.Interfaces;
using ITIApi.BL.Repositories;
using ITIApi.DAL.Data.Context;

namespace ITIApi.BL.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ITIContext context;

        private readonly Dictionary<Type, object> repos = new Dictionary<Type, object>();

        public UnitOfWork(ITIContext context)
        {
            this.context = context;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            if (!repos.ContainsKey(typeof(T)))
            {
                var repo = new Repository<T>(context);
                repos.Add(typeof(T), repo);
            }
            return (IRepository<T>)repos[typeof(T)];
        }
        public int Complete() => context.SaveChanges();

        public void Dispose() => context.Dispose();

    }
}
