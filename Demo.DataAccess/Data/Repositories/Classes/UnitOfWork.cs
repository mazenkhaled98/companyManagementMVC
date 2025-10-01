using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Data.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Demo.DataAccess.Data.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        private readonly ApplicationDbContext _applicationDbContext;
        public UnitOfWork(IEmployeeRepository employeeRepository,IDepartmentRepository departmentRepository, ApplicationDbContext applicationDbContext)
        {
            
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(applicationDbContext));
            _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(applicationDbContext));
            _applicationDbContext = applicationDbContext;
        }
        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

        public IDepartmentRepository DepartmentRepository =>_departmentRepository.Value;

       

        public int SaveChanges()
        {
            return _applicationDbContext.SaveChanges();

        }
    }
}
