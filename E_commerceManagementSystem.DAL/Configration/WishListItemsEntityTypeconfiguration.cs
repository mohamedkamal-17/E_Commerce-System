﻿using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.DAL.Configration
{
    public class WishListItemsEntityTypeconfiguration : IEntityTypeConfiguration<WishListItems>
    {
        public void Configure(EntityTypeBuilder<WishListItems> builder)
        {
           
           builder.HasKey(wli => wli.Id);
            builder.HasOne(wli => wli.Product) // Many-to-One relationship with Product
            .WithMany() // Assuming Product does not have a collection of WishListItems
            .HasForeignKey(wli => wli.ProductId); // Foreign Key
        }
    }
}