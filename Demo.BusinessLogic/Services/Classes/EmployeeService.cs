using AutoMapper;
using Demo.BusinessLogic.DTOS.EmployeeDtos;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.BusniessLogic.Dtos;
using Demo.DataAccess.Data.Repositories.Interfaces;
using Demo.DataAccess.Models.EmployeeModule;

namespace Demo.BusinessLogic.Services.Classes
{
    public class EmployeeService(IEmployeeRepository _employeeRepository,IMapper _mapper) : IEmployeeService
    {
        public int CreateEmployee(CreateEmployeeDto employeeDto)
        {
         var employee=  _mapper.Map<CreateEmployeeDto,Employee >(employeeDto);
            return _employeeRepository.Add(employee);

        }

        public bool DeleteEmployee(int id)
        {
            //soft delete ==> set isActive to false

            var employee = _employeeRepository.GetById(id);
            if (employee == null) return false;
            else
            {
                employee.IsDeleted = true;
               return _employeeRepository.Update(employee) > 0 ? true :false;
            }

        }

        public IEnumerable<EmployeeDto> GetAllEmployees(bool withTracking = false)
        {
            var employees = _employeeRepository.GetAll(withTracking);
            return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
      

        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
           var employee= _employeeRepository.GetById(id);
            if (employee == null) return null;
            else
                return _mapper.Map<Employee, EmployeeDetailsDto>(employee);
           
        }

        public int UpdateEmployee(UpdateEmployeeDto employee)
        {
          return _employeeRepository.Update(_mapper.Map<UpdateEmployeeDto,Employee>(employee));
         
        }
    }
}
