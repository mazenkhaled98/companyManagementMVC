using Demo.BusinessLogic.DTOS.DepartmentDtos;
using Demo.BusinessLogic.Factories;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Data.Repositories.Interfaces;
using Demo.DataAccess.Models;

namespace Demo.BusinessLogic.Services.Classes
{
    public class DepartmentService(IDepartmentRepository _departmentRepository) : IDepartmentService
    {
        //get all departments
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAll();
            return departments.Select(d => d.ToDepartmentDto());

        }
        //get department by id

        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department == null) return null;
            //map department to department details dto
            return department.ToDepartmentDetailsDto();
        }


        //add department
        public int AddDepartment(CreateDepartmentDto department) => _departmentRepository.Add(department.ToEntity());

        //update department
        public int UpdateDepartment(UpdateDepartmentDto department)
        {
            return _departmentRepository.Update(department.ToEntity());
        }
        //delete department
        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department == null) return false;
            int numOfRows = _departmentRepository.Delete(department);
            return numOfRows > 0;

        }


    }
}
