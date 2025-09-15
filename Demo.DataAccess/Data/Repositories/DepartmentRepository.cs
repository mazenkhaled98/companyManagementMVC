using Demo.DataAccess.Data.Contexts;

namespace Demo.DataAccess.Data.Repositories
{
    internal class DepartmentRepository(ApplicationDbContext _dbContext)
    {

        //get all departments

        //get department by id

        public Department? GetById(int id)
        {

        var department = _dbContext.Departments.Find(id);
        return department;

        }



        //add department

        //update department

        //delete department


    }
}
