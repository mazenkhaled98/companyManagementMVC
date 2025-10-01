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
                return _dbContext.Set<T>().Where(e=>e.IsDeleted==false).ToList();
            }
            else
            {
                return _dbContext.Set<T>().Where(e => e.IsDeleted == false).AsNoTracking().ToList();
            }

        }

        //get department by id

        public T? GetById(int id) => _dbContext.Set<T>().Find(id);






        //add department
        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);


        }

        //update department
        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            

        }

        //delete department
        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            

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

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate)
        {

           return _dbContext.Set<T>().Where(predicate).Where(e=>e.IsDeleted==false).ToList();

        }
    }
}
