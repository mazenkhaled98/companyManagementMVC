using System.ComponentModel.DataAnnotations;

namespace Demo.BusinessLogic.DTOS.DepartmentDtos
{
    public class CreateDepartmentDto
    {
        [Required] //name is required

        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage ="code is required !!")]

        public string Code { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateOnly DateOfCreation { get; set; }
    }
}
