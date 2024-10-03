using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerceManagementSystem.DAL.Data.Models;

namespace E_commerceManagementSystem.DAL.Configration
{
    internal class CartProductEntityTypeConfigration : IEntityTypeConfiguration<CartProduct>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CartProduct> builder)
        {

            builder.HasKey(o => new { o.ProductId, o.ShoppingCartID });
            builder.HasOne(o => o.ShoppingCart)
                 .WithMany(o => o.ShoppingCartProduct)
                  .HasForeignKey(o => o.ShoppingCartID);

            builder.HasOne(o => o.Product)
                .WithMany(o => o.ShoppingCartProduct)
                 .HasForeignKey(o => o.ProductId);


        }
    }
}
