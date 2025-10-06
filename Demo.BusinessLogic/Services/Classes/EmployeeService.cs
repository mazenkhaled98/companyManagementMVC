using AutoMapper;
using Demo.BusinessLogic.DTOS.EmployeeDtos;
using Demo.BusinessLogic.Services.AttachmentService.Interfaces;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.BusniessLogic.Dtos;
using Demo.DataAccess.Data.Repositories.Interfaces;
using Demo.DataAccess.Models.EmployeeModule;

namespace Demo.BusinessLogic.Services.Classes
{
    public class EmployeeService(IUnitOfWork _unitOfWork,IMapper _mapper ,IAttachmentService _attachmentService) : IEmployeeService
    {
        


        public int CreateEmployee(CreateEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<CreateEmployeeDto, Employee>(employeeDto);
            if (employeeDto.Image is not null)
            {
               string? imageName= _attachmentService.upload(employeeDto.Image,"images");
                employee.ImageName = imageName;
            }
           
             _unitOfWork.EmployeeRepository.Add(employee);

            return _unitOfWork.SaveChanges();

        }

        public bool DeleteEmployee(int id)
        {
            //soft delete ==> set isActive to false

            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee == null) return false;
            else
            {
                employee.IsDeleted = true;
                 _unitOfWork.EmployeeRepository.Update(employee) ;
                return _unitOfWork.SaveChanges() > 0 ? true : false;
            }

        }

        public IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName, bool withTracking = false)
        {

            var employeeRepo = _unitOfWork.EmployeeRepository;
            IEnumerable<Employee> employees;
            if (String.IsNullOrEmpty(EmployeeSearchName))
            {
                 employees = employeeRepo.GetAll(withTracking);
               
            }
            else
            {
                 employees = employeeRepo.GetAll(e => e.Name.ToLower().Contains( EmployeeSearchName.ToLower()));
                
            }
            return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);

        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
           var employee= _unitOfWork.EmployeeRepository.GetById(id);
            if (employee == null) return null;
            else
                return _mapper.Map<Employee, EmployeeDetailsDto>(employee);
           
        }

        public int UpdateEmployee(UpdateEmployeeDto employee)
        {
           _unitOfWork.EmployeeRepository.Update(_mapper.Map<UpdateEmployeeDto,Employee>(employee));
         return _unitOfWork.SaveChanges();
        }
    }
}
