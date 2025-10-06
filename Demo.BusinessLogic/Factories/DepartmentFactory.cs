using Demo.BusinessLogic.DTOS.DepartmentDtos;
using Demo.DataAccess.Models.DepartmentModule;

namespace Demo.BusinessLogic.Factories
{
    internal static class DepartmentFactory
    {

        public static DepartmentDto ToDepartmentDto(this Department department)
        {
           
            return new DepartmentDto
            {
                DeptId = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description ?? string.Empty,
                DateOfCreation = department.CreatedOn.HasValue ? DateOnly.FromDateTime(department.CreatedOn.Value) : default
            };
        }

        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
        {
            return new DepartmentDetailsDto
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreatedBy = department.CreatedBy,
                CreatedOn = department.CreatedOn.HasValue ? DateOnly.FromDateTime(department.CreatedOn.Value) : default,
                ModifedBy = department.ModifedBy,
                ModifiedOn = department.ModifiedOn.HasValue ? DateOnly.FromDateTime(department.ModifiedOn.Value) : default,
                IsDeleted = department.IsDeleted
            };
        }

        public static Department ToEntity(this CreateDepartmentDto createDepartmentDto)
        {
            return new Department
            {
                Name = createDepartmentDto.Name,
                Code = createDepartmentDto.Code,
                Description = createDepartmentDto.Description,
                CreatedOn = createDepartmentDto.DateOfCreation.ToDateTime(new TimeOnly())
            };
        }

        public static Department ToEntity(this UpdateDepartmentDto createDepartmentDto)
        {
            return new Department
            {
                Id= createDepartmentDto.Id,
                Name = createDepartmentDto.Name,
                Code = createDepartmentDto.Code,
                Description = createDepartmentDto.Description,
                CreatedOn = createDepartmentDto.DateOfCreation.ToDateTime(new TimeOnly())
            };
        }


    }
}
