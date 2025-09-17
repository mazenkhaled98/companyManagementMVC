using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Data.Repositories.Interfaces;
using Demo.DataAccess.Models.DepartmentModule;

namespace Demo.DataAccess.Data.Repositories.Classes
{
    public class DepartmentRepository(ApplicationDbContext _dbContext) :GenericRepository<Department>(_dbContext), IDepartmentRepository
    {
        //methods specific to Department entity
    }
}
