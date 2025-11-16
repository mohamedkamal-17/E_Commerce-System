using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_commerceManagementSystem.DAL.Configration
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {


            builder.HasKey(a => a.Id);

            builder.HasOne(s => s.Category)               // One product is enrolled in one category
           .WithMany(c => c.Products)           // One category can have many product
           .HasForeignKey(s => s.CategoryId);     // Define CategoryID as the foreign key

            // one to one relation between shoppingCart and products




            // one to one relation between inventory and products
            builder.HasOne(a => a.Inventory)
           .WithOne(ab => ab.Product)
           .HasForeignKey<Inventory>(ab => ab.ProductId);
        }
    }
}

