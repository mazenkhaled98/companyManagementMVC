using Demo.DataAccess.Models.EmployeeModule;
using Demo.DataAccess.Models.Shared;
using System.Linq.Expressions;

namespace Demo.DataAccess.Data.Repositories.Interfaces
{
    public interface IGenericRepository< T> where T : BaseEntity 
    {
        void Add(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll(bool withTracking = false);

        IEnumerable<T> GetAll(Expression<Func<T,bool>> predicate);
        IEnumerable<TResult> GetAll<TResult>(Expression<Func<T,TResult>>selector);
        T? GetById(int id);
        void Update(T entity);

        IEnumerable<T> GetIEnumrable();

        IQueryable<T> GetIQueryable();

    }
    
    
}
