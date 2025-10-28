using System.Linq.Expressions;

namespace ITIApi.BL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        T GetById(int id);
        void Delete(T entity);
        void Update(T entity);
        void Add(T entity);
    }
}
