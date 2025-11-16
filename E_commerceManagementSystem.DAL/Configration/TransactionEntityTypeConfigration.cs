using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerceManagementSystem.DAL.Data.Models;


namespace E_commerceManagementSystem.DAL.Configration
{
    public class TransactionEntityTypeConfigration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(t => t.TransactionId);

            builder.Property(t => t.Amount)
                  .HasPrecision(18, 2);

            builder.Property(t => t.Currency)
                  .HasMaxLength(3)
                  .IsRequired();

            // Gateway fields
            builder.Property(t => t.GatewayName)
                  .HasMaxLength(50)
                  .IsRequired();

            builder.Property(t => t.GatewayTransactionId)
                  .HasMaxLength(200);

            builder.Property(t => t.GatewayRawResponse)
                  .HasColumnType("nvarchar(max)")
                  .IsRequired();

            // Payment method details
            builder.Property(t => t.MaskedCardNumber)
                  .HasMaxLength(20);

            builder.Property(t => t.PaymentMethod)
                  .HasMaxLength(50);

            builder.Property(t => t.CardBrand)
                  .HasMaxLength(50);

            // Error handling
            builder.Property(t => t.ErrorMessage)
                  .HasMaxLength(1000);

            builder.Property(t => t.ErrorCode)
                  .HasMaxLength(100);

            // Metadata
            builder.Property(t => t.Metadata)
                  .HasColumnType("nvarchar(max)");

            // Foreign key relationship
            builder.HasOne(t => t.Payment)
                  .WithMany(p => p.Transactions)
                  .HasForeignKey(t => t.PaymentId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(t => t.PaymentId);
            builder.HasIndex(t => t.GatewayTransactionId);
            builder.HasIndex(t => t.GatewayName);
            builder.HasIndex(t => new { t.GatewayName, t.GatewayTransactionId });
            builder.HasIndex(t => t.ProcessedAt);
        }
    }
}
