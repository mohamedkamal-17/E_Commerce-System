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
