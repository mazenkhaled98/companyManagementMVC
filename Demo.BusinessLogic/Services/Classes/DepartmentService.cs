using Demo.BusinessLogic.DTOS.DepartmentDtos;
using Demo.BusinessLogic.Factories;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Data.Repositories.Interfaces;

namespace Demo.BusinessLogic.Services.Classes
{
    public class DepartmentService(IUnitOfWork _unitOfWork ) : IDepartmentService
    {
        //get all departments
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            return departments.Select(d => d.ToDepartmentDto());

        }
        //get department by id

        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            if (department == null) return null;
            //map department to department details dto
            return department.ToDepartmentDetailsDto();
        }


        //add department
        public int AddDepartment(CreateDepartmentDto department)
        {
            _unitOfWork.DepartmentRepository.Add(department.ToEntity());
            return _unitOfWork.SaveChanges();

        }

        //update department
        public int UpdateDepartment(UpdateDepartmentDto department)
        {
             _unitOfWork.DepartmentRepository.Update(department.ToEntity());
            return _unitOfWork.SaveChanges();
        }
        //delete department
        public bool DeleteDepartment(int id)
        {

            var department = _unitOfWork.DepartmentRepository.GetById(id);
            if (department == null) return false;
            _unitOfWork.DepartmentRepository.Delete(department);
            return _unitOfWork.SaveChanges() > 0 ? true : false;

        }


    }
}
