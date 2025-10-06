using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Data.Repositories.Interfaces;
using Demo.DataAccess.Models.DepartmentModule;
using Demo.DataAccess.Models.EmployeeModule;
using Microsoft.EntityFrameworkCore;

namespace Demo.DataAccess.Data.Repositories.Classes
{
    public class EmployeeRepository(ApplicationDbContext _dbContext) :GenericRepository<Employee>(_dbContext),  IEmployeeRepository
    {
        

    }
}
