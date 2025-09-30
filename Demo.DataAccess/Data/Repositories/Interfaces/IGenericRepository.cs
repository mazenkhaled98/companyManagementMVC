using Demo.DataAccess.Models.EmployeeModule;
using Demo.DataAccess.Models.Shared;
using System.Linq.Expressions;

namespace Demo.DataAccess.Data.Repositories.Interfaces
{
    public interface IGenericRepository< T> where T : BaseEntity 
    {
        int Add(T entity);
        int Delete(T entity);
        IEnumerable<T> GetAll(bool withTracking = false);

        IEnumerable<T> GetAll(Expression<Func<T,bool>> predicate);
        IEnumerable<TResult> GetAll<TResult>(Expression<Func<T,TResult>>selector);
        T? GetById(int id);
        int Update(T entity);

        IEnumerable<T> GetIEnumrable();

        IQueryable<T> GetIQueryable();

    }
    
    
}
