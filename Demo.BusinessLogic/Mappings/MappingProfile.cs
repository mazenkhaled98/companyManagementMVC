using AutoMapper;
using Demo.BusinessLogic.DTOS.EmployeeDtos;
using Demo.BusniessLogic.Dtos;
using Demo.DataAccess.Models.EmployeeModule;

namespace Demo.BusinessLogic.Mappings
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {

            CreateMap<Employee, EmployeeDto>()
                .ForMember(destination=>destination.Gender,options=>options.MapFrom(src=>src.Gender)).
                ForMember(destination=>destination.EmployeeType,options=>options.MapFrom(src=>src.Employeetype))
                .ForMember(dest=>dest.DepartmentName,options=>options.MapFrom(src=>src.Department != null ? src.Department.Name : null ));

            CreateMap<Employee, EmployeeDetailsDto>().ForMember(destination => destination.Gender, options => options.MapFrom(src => src.Gender)).
                ForMember(destination => destination.EmployeeType, options => options.MapFrom(src => src.Employeetype))
                .ForMember(destination=>destination.HiringDate ,options=>options.MapFrom(src =>DateOnly.FromDateTime(src.HiringDate)))
                 .ForMember(dest => dest.DepartmentName, options => options.MapFrom(src => src.Department != null ? src.Department.Name : null)); ;

            //CreateMap<Employee, EmployeeDetailsDto>().ReverseMap();
            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));

            CreateMap<UpdateEmployeeDto, Employee>().ForMember(dest => dest.HiringDate, options => options.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));

        }
    }
}
