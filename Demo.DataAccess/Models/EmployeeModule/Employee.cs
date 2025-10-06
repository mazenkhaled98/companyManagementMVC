using Demo.DataAccess.Models.DepartmentModule;
using Demo.DataAccess.Models.Shared;

namespace Demo.DataAccess.Models.EmployeeModule
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string? Address { get; set; }
       
        public bool IsActive { get; set; }
        public decimal Salary { get; set; }

        public string? Email { get; set; }

        public string? Phonenumber { get; set; }
        public DateTime HiringDate { get; set; }


        //Gender ==> [female or male]
        public Gender Gender { get; set; }

        //employeetype ==> [parttimeemployee,fulltimeemployee]
        public EmployeeType Employeetype { get; set; }

        public virtual Department? Department { get; set; }

        public int? DepartmentId { get; set; }

        public string? ImageName { get; set; }
    }
}
