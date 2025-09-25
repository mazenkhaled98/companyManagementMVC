using Demo.DataAccess.Models.DepartmentModule;
using Demo.DataAccess.Models.EmployeeModule;
using Demo.DataAccess.Models.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.DataAccess.Data.Configurations
{
    internal class EmployeeConfigurations : BaseEntityConfigurations<Employee>, IEntityTypeConfiguration<Employee>
    {
        public new void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E=>E.Address).HasColumnType("nvarchar(50)");
            builder.Property(E=>E.Salary).HasColumnType("nvarchar(50)");
            builder.Property(E=>E.Name).HasColumnType("nvarchar(50)");
            builder.Property(E=>E.Gender).HasConversion((empGender)=>empGender.ToString(),
                (gender)=>(Gender)Enum.Parse(typeof(Gender),gender));

            builder.Property(E => E.Employeetype).HasConversion((empType) => empType.ToString(),
             (employeeType) => (EmployeeType)Enum.Parse(typeof(EmployeeType), employeeType));

            base.Configure(builder);
        }
    }
}
