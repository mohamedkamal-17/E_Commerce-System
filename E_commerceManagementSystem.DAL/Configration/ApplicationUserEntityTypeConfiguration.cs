using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_commerceManagementSystem.DAL.Configration
{
    public class ApplicationUserEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {




            // one to one relation between shoppingCart and users
            builder.HasOne(a => a.ShoppingCart)
           .WithOne(ab => ab.User)
           .HasForeignKey<Cart>(ab => ab.UserId);


        }
    }
}
