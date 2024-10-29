using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_commerceManagementSystem.DAL.Configration
{
    public class InventoryEntityTypeConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.HasKey(a => a.Id);

        }
    }

}
