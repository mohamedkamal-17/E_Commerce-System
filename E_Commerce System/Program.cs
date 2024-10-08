using E_commerceManagementSystem.DAL.Data.Dphelper;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.BLL.DTOs;


using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using E_commerceManagementSystem.BLL.Manager.AccountManager;
using E_commerceManagementSystem.BLL.Manager.JwtTokenManager;
using E_commerceManagementSystem.BLL.Manager.ProductManager;
using E_commerceManagementSystem.DAL.Reposatories.ProductRepository;
using E_commerceManagementSystem.DAL.Repositories.Classes;
using E_commerceManagementSystem.DAL.Reposatories.ReviewRepository;

using E_commerceManagementSystem.BLL.Manager.ReviewManager;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Reposatories.OrederRepository;
using E_commerceManagementSystem.BLL.Manager.OrderManager;
using E_commerceManagementSystem.DAL.Reposatories.OrederItemRepository;
using E_commerceManagementSystem.BLL.Manager.OrderItemManager;
using E_commerceManagementSystem.BLL.Manager.CartItemManager;
using E_commerceManagementSystem.DAL.Reposatories.CartItemRepository;
using E_commerceManagementSystem.BLL.Manager.CartManager;
using E_commerceManagementSystem.DAL.Reposatories.CartRepository;
using E_commerceManagementSystem.BLL.Manager.OtpManager;
using E_commerceManagementSystem.BLL.Manager.EmailManager;


namespace E_Commerce_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.





            builder.Services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("E_Commerce_string"));
            });
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllers();

            builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
            builder.Services.AddScoped<IAccountManager, AccountMangare>();


            builder.Services.AddScoped<IProductMangare,ProductManger>();
            builder.Services.AddScoped<IProductRepo, ProductRepo>();

            builder.Services.AddScoped<IReviewManager, ReviewManager>();
            builder.Services.AddScoped<IReviewRepo, ReviewRepo>();

            builder.Services.AddScoped<IOrderRepo, OrderRepo>();
            builder.Services.AddScoped<IOrderManager, OrderManager>();

            builder.Services.AddScoped<IOrderItemRepo, OrderItemRepo>();
            builder.Services.AddScoped<IOrderItemManager, OrderItemManager>();

            builder.Services.AddScoped<ICartManager, CartManager>();
            builder.Services.AddScoped<ICartRepo, CartRepo>();

            builder.Services.AddScoped<ICartItemManager, CartItemManager>();
            builder.Services.AddScoped<ICartItemRepo, CartItemRepo>();

            builder.Services.AddScoped<IOtpManager,OtpManager>();
            builder.Services.AddScoped<IEmailManager, EmailManager>();
            builder.Services.AddMemoryCache();


            builder.Services.AddAutoMapper(typeof(Program));

            // register for the service 
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
