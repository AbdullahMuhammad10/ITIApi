using ITIApi.BL.Interfaces;
using ITIApi.DAL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ITIApi.BL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private protected readonly ITIContext context;
        private protected readonly DbSet<T> Set;

        public Repository(ITIContext context)
        {
            this.context = context;
            Set = context.Set<T>();
        }
        public void Add(T entity) => Set.Add(entity);

        public void Delete(T entity) => Set.Remove(entity);

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate) => Set.Where(predicate);

        public IQueryable<T> GetAll() => Set;

        public T GetById(int id) => Set.Find(id);
        public void Update(T entity) => Set.Update(entity);
    }
}
