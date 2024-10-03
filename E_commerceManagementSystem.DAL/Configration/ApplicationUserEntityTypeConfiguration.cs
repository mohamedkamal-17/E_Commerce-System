using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace E_commerceManagementSystem.DAL.Configration
{
    public class ApplicationUserEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
         //   var encryptConverter = new ValueConverter<string, string>(
         //v => Data_Encryption_Algorathim.EncryptionHelper.Encrypt(v),   // Encrypt before saving to the database
         //v => Data_Encryption_Algorathim.EncryptionHelper.Decrypt(v)    // Decrypt after reading from the database
         //);

         //   builder.Property(a=>a.Password).HasConversion(encryptConverter);
         //   builder.HasKey(a => a.UserID);
         //   builder.HasIndex(a=>a.Email).IsUnique();



            // one to one relation between shoppingCart and users
            builder.HasOne(a => a.ShoppingCart)
           .WithOne(ab => ab.User)
           .HasForeignKey<Cart>(ab => ab.UserId);


        }
    }
}
