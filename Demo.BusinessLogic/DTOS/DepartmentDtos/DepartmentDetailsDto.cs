using Demo.DataAccess.Models;

namespace Demo.BusinessLogic.DTOS.DepartmentDtos
{
    public class DepartmentDetailsDto
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; } //user id
        public DateOnly? CreatedOn { get; set; } //date time of creation

        public int ModifedBy { get; set; }

        public DateOnly? ModifiedOn { get; set; } //the date time of modification

        public bool IsDeleted { get; set; } //soft delete

        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }


   
    }
}
