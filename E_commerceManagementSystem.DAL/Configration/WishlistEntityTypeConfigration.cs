using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.DAL.Configration
{
    internal class WishlistEntityTypeConfigration : IEntityTypeConfiguration<WishList>
    {
        public void Configure(EntityTypeBuilder<WishList> builder)
        {
            builder.HasOne(o => o.User)
                .WithMany(o => o.WishList)
                .HasForeignKey(o => o.UserId);

            builder.HasOne(o => o.Product)
                .WithMany(o => o.WishList)
                .HasForeignKey(o => o.ProductID);

        }
    }
}
