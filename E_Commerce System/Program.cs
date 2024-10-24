using E_commerceManagementSystem.DAL.Data.Dphelper;
using E_commerceManagementSystem.DAL.Data.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using E_commerceManagementSystem.BLL.Manager.AccountManager;
using E_commerceManagementSystem.BLL.Manager.JwtTokenManager;
using E_commerceManagementSystem.BLL.Manager.ProductManager;
using E_commerceManagementSystem.DAL.Reposatories.ProductRepository;
using E_commerceManagementSystem.DAL.Repositories.Classes;
using E_commerceManagementSystem.DAL.Reposatories.ReviewRepository;

using E_commerceManagementSystem.BLL.Manager.ReviewManager;
using E_commerceManagementSystem.DAL.Reposatories.OrederRepository;
using E_commerceManagementSystem.BLL.Manager.OrderManager;
using E_commerceManagementSystem.DAL.Reposatories.OrederItemRepository;
using E_commerceManagementSystem.BLL.Manager.OrderItemManager;
using E_commerceManagementSystem.DAL.Reposatories.InventoryRepository;
using E_commerceManagementSystem.BLL.Manager.InventoryManager;
using E_commerceManagementSystem.DAL.Reposatories.ShippingRepository;
using E_commerceManagementSystem.BLL.Manager.ShippingManger;
using E_commerceManagementSystem.DAL.Reposatories.PaymebtRepository;
using E_commerceManagementSystem.BLL.Manager.PaymentManager;
using E_commerceManagementSystem.DAL.Reposatories.CategoryRepository;
using E_commerceManagementSystem.BLL.Manager.CategoryManger;
using E_commerceManagementSystem.DAL.Reposatories.WishlistRepsitory;
using E_commerceManagementSystem.BLL.Manager.WishlistItemsManager;
using E_commerceManagementSystem.BLL.Manager.WishlistManager;
using E_commerceManagementSystem.DAL.Reposatories.WishListItemsRepository;
using E_commerceManagementSystem.BLL.Manager.ShippingManager;
using E_commerceManagementSystem.BLL.Manager.CategoryManager;
using E_commerceManagementSystem.BLL.Manager.CartItemManager;
using E_commerceManagementSystem.DAL.Reposatories.CartItemRepository;
using E_commerceManagementSystem.BLL.Manager.CartManager;
using E_commerceManagementSystem.DAL.Reposatories.CartRepository;
using E_commerceManagementSystem.BLL.Manager.OtpManager;
using E_commerceManagementSystem.BLL.Manager.EmailManager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using E_commerceManagementSystem.BLL.AutoMapper;
using Microsoft.OpenApi.Models;
using E_commerceManagementSystem.BLL;


namespace E_Commerce_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.AllowedUserNameCharacters =
                     "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._ ";
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;         
                options.Password.RequireLowercase = false;     
                options.Password.RequireUppercase = false;      
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 4;            
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(option =>
            {
                option.SaveToken = true;
                option.RequireHttpsMetadata = false;
                option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = "https://localhost:7131/",
                    ValidateAudience = true,
                    ValidAudience = "https://localhost:7131/",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("y9XQ!@324fkpq34Vn04i5#W6$%fTgQwErTgBhYtNmQqPzXqFjKl09"))
                };
            });

            builder.Services.AddControllers();

            builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));


            builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
            builder.Services.AddScoped<IAccountManager, AccountManager>();


            builder.Services.AddScoped<IProductMangare, ProductManager>();
            builder.Services.AddScoped<IProductRepo, ProductRepo>();

            builder.Services.AddScoped<IReviewManager, ReviewManager>();
            builder.Services.AddScoped<IReviewRepo, ReviewRepo>();

            builder.Services.AddScoped<IOrderRepo, OrderRepo>();
            builder.Services.AddScoped<IOrderManager, OrderManager>();

            builder.Services.AddScoped<IOrderItemRepo, OrderItemRepo>();
            builder.Services.AddScoped<IOrderItemManager, OrderItemManager>();

            builder.Services.AddScoped<IInventoryRepo, InventoryRepo>();
            builder.Services.AddScoped<IInventoryManager, InventoryManager>();

            builder.Services.AddScoped<IShippingRepo, ShippingRepo>();
            builder.Services.AddScoped<IShippingManager, ShippingManager>();

            builder.Services.AddScoped<IPaymentRepo, PaymentRepo>();
            builder.Services.AddScoped<IPaymentManger, PaymentManager>();

            builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
            builder.Services.AddScoped<ICategoryManager, CategoryManager>();

            builder.Services.AddScoped<IWishlistRepo, WishlistRepo>();
            builder.Services.AddScoped<IWishlistManager, WishlistManager>();


            builder.Services.AddScoped<IWishListItemsRepo, WishListItemsRepo>();
            builder.Services.AddScoped<IWishlistItemsManager, WishlistItemsManager>();

            builder.Services.AddScoped<ICartManager, CartManager>();
            builder.Services.AddScoped<ICartRepo, CartRepo>();

            builder.Services.AddScoped<ICartItemManager, CartItemManager>();
            builder.Services.AddScoped<ICartItemRepo, CartItemRepo>();

            builder.Services.AddScoped<IOtpManager,OtpManager>();
            builder.Services.AddScoped<IEmailManager, EmailManager>();
            builder.Services.AddMemoryCache();


            builder.Services.AddAutoMapper(typeof(MappingProfile));
             void ConfigureServices(IServiceCollection services)
            {
                services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                        options.JsonSerializerOptions.WriteIndented = true; // Optional: for better readability
                    });
            }


            // register for the service 
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

                // Define the security scheme
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter a valid token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "Bearer"
                    });

                // Apply the security requirement globally
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] { }
                        }
                    });
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleWare>();


            app.MapControllers();

            app.Run();
        }
    }
}
