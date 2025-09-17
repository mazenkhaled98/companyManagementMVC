using Demo.DataAccess.Models.EmployeeModule;
using Demo.DataAccess.Models.Shared;

namespace Demo.DataAccess.Data.Repositories.Interfaces
{
    public interface IGenericRepository< T> where T : BaseEntity 
    {
        int Add(T entity);
        int Delete(T entity);
        IEnumerable<T> GetAll(bool withTracking = false);
        T? GetById(int id);
        int Update(T entity);

    }
    
    
}
