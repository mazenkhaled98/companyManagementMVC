using Demo.DataAccess.Models.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.DataAccess.Data.Configurations
{
    internal class BaseEntityConfigurations<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(D => D.CreatedOn).HasDefaultValueSql("getdate()");

            builder.Property(D => D.ModifiedOn).HasComputedColumnSql("getdate()");

            
        }
    }
    
}
