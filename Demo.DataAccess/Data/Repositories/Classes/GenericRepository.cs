using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Data.Repositories.Interfaces;
using Demo.DataAccess.Models.DepartmentModule;
using Demo.DataAccess.Models.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Demo.DataAccess.Data.Repositories.Classes
{
    public class GenericRepository<T>(ApplicationDbContext _dbContext) : IGenericRepository<T> where T : BaseEntity    
    {

        //get all departments

        public IEnumerable<T> GetAll(bool withTracking = false)
        {
            if (withTracking)
            {
                return _dbContext.Set<T>().ToList();
            }
            else
            {
                return _dbContext.Set<T>().AsNoTracking().ToList();
            }

        }

        //get department by id

        public T? GetById(int id) => _dbContext.Set<T>().Find(id);






        //add department
        public int Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return _dbContext.SaveChanges();

        }

        //update department
        public int Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();

        }

        //delete department
        public int Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return _dbContext.SaveChanges();

        }

        public IEnumerable<T> GetIEnumrable()
        {
           return _dbContext.Set<T>();
        }

        public IQueryable<T> GetIQueryable()
        {
            return _dbContext.Set<T>();
        }

        public IEnumerable<TResult> GetAll<TResult>(Expression<Func<T, TResult>> selector)
        {
           return  _dbContext.Set<T>().Where(entity=>entity.IsDeleted==false)
                .Select(selector).ToList();
        }
    }
}
