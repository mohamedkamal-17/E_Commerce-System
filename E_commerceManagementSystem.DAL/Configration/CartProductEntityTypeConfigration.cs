using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerceManagementSystem.DAL.Configration
{
    internal class CartProductEntityTypeConfigration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CartItem> builder)
        {

            builder.HasOne(o => o.Cart)
                 .WithMany(o => o.CartItems)
                  .HasForeignKey(o => o.CartID);

            builder.HasOne(o => o.Product)
                .WithMany(o => o.CartItem)
                 .HasForeignKey(o => o.ProductId);


        }
    }
}
