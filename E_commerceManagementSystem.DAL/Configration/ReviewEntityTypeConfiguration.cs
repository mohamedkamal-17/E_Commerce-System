using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_commerceManagementSystem.DAL.Configration
{
    public class ReviewEntityTypeConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(a => a.Id);

            // One to many relationship between product and reviews
            builder.HasOne(s => s.Product)
               .WithMany(c => c.Reviews)
               .HasForeignKey(s => s.ProductId);

            // One to many relationship between product and reviews
            builder.HasOne(s => s.User)
             .WithMany(c => c.Reviews)
             .HasForeignKey(s => s.UserId);

        }
    }

}
