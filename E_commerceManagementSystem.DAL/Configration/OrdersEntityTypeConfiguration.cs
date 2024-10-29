using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_commerceManagementSystem.DAL.Configration
{
    public class OrdersEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(a => a.Id);

            // one to many relationship between users and orders 
            builder.HasOne(a => a.User)
            .WithMany(b => b.Orders)
            .HasForeignKey(c => c.UserId);




        }
    }
}

