using Demo.BusinessLogic.DTOS.EmployeeDtos;
using Demo.BusniessLogic.Dtos;

namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IEmployeeService
    {
        //Get all employees
        IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName,bool withTracking = false);

        //get by id
        EmployeeDetailsDto? GetEmployeeById(int id);

        //create employee
        int CreateEmployee(CreateEmployeeDto employee);

        //update employee
        int UpdateEmployee(UpdateEmployeeDto employee);

        //delete employee
        bool DeleteEmployee(int id);
    }
}
