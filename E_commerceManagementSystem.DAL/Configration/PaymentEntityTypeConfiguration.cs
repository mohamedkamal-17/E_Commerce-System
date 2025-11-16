using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_commerceManagementSystem.DAL.Configration
{

    public class PaymentEntityTypeConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(p => p.Amount)
                .HasPrecision(18, 2);
            builder.Property(p => p.Currency)
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(p => p.GatewayName)
             .HasMaxLength(50);
            builder.Property(p => p.SelectedPaymentMethod)
              .HasMaxLength(50);
            builder.Property(p => p.GatewayOrderId)
                  .HasMaxLength(200); 

            builder.Property(p => p.GatewayPaymentUrl)
                  .HasMaxLength(2000);  


            builder.Property(p => p.CustomerEmail)
                  .HasMaxLength(255)
                  .IsRequired();

            builder.Property(p => p.CustomerFirstName)
                  .HasMaxLength(100)
                  .IsRequired();

            builder.Property(p => p.CustomerLastName)
                  .HasMaxLength(100)
                  .IsRequired();

            builder.Property(p => p.CustomerPhone)
                  .HasMaxLength(20)
                  .IsRequired();

            builder.Property(p => p.Metadata)
                  .HasColumnType("nvarchar(max)");

            //builder.HasOne(p => p.Order)
            //      .WithOne(o => o.Payment)
            //      .HasForeignKey<Payment>(p => p.OrderId)
            //      .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => p.OrderId).IsUnique();
            builder.HasIndex(p => p.GatewayOrderId);
            builder.HasIndex(p => p.GatewayName);
            builder.HasIndex(p => new { p.Status, p.CreatedAt });
            builder.HasIndex(p => new { p.GatewayName, p.GatewayOrderId });  // Composite index
        }
    } 
}

