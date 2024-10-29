using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_commerceManagementSystem.DAL.Data.Dphelper
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationUser).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Product).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Category).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Order).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderItem).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Payment).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Cart).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Inventory).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Review).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CartItem).Assembly);


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Shipping> Shipping { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<WishList> WishLists { get; set; }


    }
}
