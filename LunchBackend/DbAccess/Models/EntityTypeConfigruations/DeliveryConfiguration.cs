using LunchBackend.DbAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LunchBackend.DbAccess.Models.EntityTypeConfigruations
{
    public class DeliveryConfiguration: IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            // define primary key
            builder.HasKey(d => d.Id);
                
           // define relationship
           builder.HasMany(d => d.Orders)
               .WithOne(o => o.Deliver)
               .HasForeignKey(o => o.DeliverId);
        }
    }
}