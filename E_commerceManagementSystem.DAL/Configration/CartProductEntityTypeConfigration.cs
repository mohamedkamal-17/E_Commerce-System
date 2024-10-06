using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerceManagementSystem.DAL.Data.Models;

namespace E_commerceManagementSystem.DAL.Configration
{
    internal class CartProductEntityTypeConfigration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CartItem> builder)
        {

            builder.HasKey(o => new { o.ProductId, o.CartID });
            builder.HasOne(o => o.Cart)
                 .WithMany(o => o.CartItems)
                  .HasForeignKey(o => o.CartID);

            builder.HasOne(o => o.Product)
                .WithMany(o => o.CartItem)
                 .HasForeignKey(o => o.ProductId);


        }
    }
}
