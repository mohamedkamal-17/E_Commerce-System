using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace E_commerceManagementSystem.DAL.Configration
{
    public class ShippingEntitytypeConfigration : IEntityTypeConfiguration<Shipping>
    {
        public void Configure(EntityTypeBuilder<Shipping> builder)
        {
            builder.HasOne(o => o.Order)
                 .WithOne(o => o.Shipping)
                 .HasForeignKey<Shipping>(o => o.OrderId);
        }
    }
}
