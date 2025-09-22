using Demo.BusinessLogic.DTOS.EmployeeDtos;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.BusniessLogic.Dtos;
using Demo.DataAccess.Data.Repositories.Interfaces;

namespace Demo.BusinessLogic.Services.Classes
{
    public class EmployeeService(IEmployeeRepository _employeeRepository) : IEmployeeService
    {
        public int CreateEmployee(CreateEmployeeDto employee)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmployeeDto> GetAllEmployees(bool withTracking = false)
        {
            var employees = _employeeRepository.GetAll(withTracking);
            var employeeDtos = employees.Select(e => new EmployeeDto()
            {
                Id = e.Id,
                Name = e.Name,
                Age = e.Age,
                Salary = e.Salary,
                IsActive = e.IsActive,
                Email = e.Email,
                Gender = e.Gender.ToString(),
                EmployeeType = e.Employeetype.ToString()
            });
            return employeeDtos;
        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
           var employee= _employeeRepository.GetById(id);
            if (employee == null) return null;
            else
                return new EmployeeDetailsDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    Salary = employee.Salary,
                    IsActive = employee.IsActive,
                    Email = employee.Email,
                    PhoneNumber = employee.Phonenumber,
                    HiringDate = DateOnly.FromDateTime(employee.HiringDate),
                    EmployeeType = employee.Employeetype.ToString(),
                    CreatedBy = employee.CreatedBy,
                    CreatedOn = employee.CreatedOn,
                    ModifiedBy = employee.ModifedBy,
                    ModifiedOn = employee.ModifiedOn,


                };
        }

        public int UpdateEmployee(UpdateEmployeeDto employee)
        {
            throw new NotImplementedException();
        }
    }
}
