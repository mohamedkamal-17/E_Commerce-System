using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_commerceManagementSystem.DAL.Configration
{

    public class OrdersItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasOne(s => s.Product)               // One orderItem is enrolled in one product type
           .WithMany(c => c.OrderItems)           // One product type can have many orderItem
           .HasForeignKey(s => s.ProductId);     // Define ProductID as the foreign key

            builder.HasOne(s => s.Order)               // One orderItem is enrolled in one order 
             .WithMany(c => c.OrderItems)           // One order type can have many orderItem
             .HasForeignKey(s => s.OrderId);     // Define OrderID as the foreign key


        }
    }
}

