using Demo.BusinessLogic.DTOS;

namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IDepartmentService
    {
        int AddDepartment(CreateDepartmentDto department);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetailsDto? GetDepartmentById(int id);
        int UpdateDepartment(UpdateDepartmentDto department);
    }
}