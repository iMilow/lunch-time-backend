using LunchBackend.DbAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LunchBackend.DbAccess.Models.EntityTypeConfigruations
{
    public class OrderConfigruation: IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // define primary key
            builder.HasKey(o => o.Id);

            // define relationchips
            builder.HasOne(o => o.Deliver)
                .WithMany(d => d.Orders)
                .HasForeignKey(o => o.DeliverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}