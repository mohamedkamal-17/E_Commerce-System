using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
             

            // one to one relation between orders and payments
               builder.HasOne(a => a.payment)
              .WithOne(ab => ab.orders)
              .HasForeignKey<Payment>(ab => ab.OrderId);

        }
    }
}

