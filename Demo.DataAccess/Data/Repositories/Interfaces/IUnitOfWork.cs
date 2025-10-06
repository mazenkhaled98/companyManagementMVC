namespace Demo.DataAccess.Data.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public IEmployeeRepository EmployeeRepository { get; }

        public IDepartmentRepository DepartmentRepository { get; }

        //save changes
        public int SaveChanges();
    }
}
