namespace Demo.DataAccess.Data.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        int Add(Department department);
        int Delete(Department department);
        IEnumerable<Department> GetAll(bool withTracking = false);
        Department? GetById(int id);
        int Update(Department department);
    }
}