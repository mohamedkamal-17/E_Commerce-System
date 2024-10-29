using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.CartDto;
using E_commerceManagementSystem.BLL.Dto.CartItemDto;
using E_commerceManagementSystem.BLL.Dto.CategoryDto;
using E_commerceManagementSystem.BLL.Dto.InventoryDto;
using E_commerceManagementSystem.BLL.Dto.OrderDto;
using E_commerceManagementSystem.BLL.Dto.OrederItemDto;
using E_commerceManagementSystem.BLL.Dto.PaymentDto;
using E_commerceManagementSystem.BLL.Dto.ProductDto;
using E_commerceManagementSystem.BLL.Dto.ReviewDto;
using E_commerceManagementSystem.BLL.Dto.ShippingDto;
using E_commerceManagementSystem.BLL.Dto.WishlistDto;
using E_commerceManagementSystem.BLL.Dto.WishListItemsDto;
using E_commerceManagementSystem.DAL.Data.Models;
using Stripe;
using Product = E_commerceManagementSystem.DAL.Data.Models.Product;
using Review = E_commerceManagementSystem.DAL.Data.Models.Review;
using Shipping = E_commerceManagementSystem.DAL.Data.Models.Shipping;

namespace E_commerceManagementSystem.BLL.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Product Mappings
            // Map from Product to ProductReadDto
            CreateMap<Product, ReadProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
                .ReverseMap();

            // Map from ProductAddDto to Product
            CreateMap<AddProductDto, Product>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ReverseMap();

            // Map from ProductUpdateDto to Product
            CreateMap<UpdateProductDto, Product>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ReverseMap();
            #endregion

            #region Order Mappings
            // Map from Order to ReadOrderDto
            //CreateMap<AddOrderDto, Order>()
            //.ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems)) // Map OrderItems
            //.ForMember(dest => dest.ArrivalDate, opt => opt.MapFrom(src => DateTime.Now.AddDays(10))) // Default ArrivalDate
            //.ForMember(dest => dest.TotalPrice, opt =>
            //opt.MapFrom(src => src.OrderItems.Sum(ci => ci.Product.Price * ci.Quantity)));

            // Mapping from Order to ReadOrderDto
            CreateMap<Order, ReadOrderDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.UserName : string.Empty))
                           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            ;
            // Compute TotalPrice from OrderItems


            CreateMap<UpdateOrderDto, Order>();
            #endregion

            #region OrderItem Mappings
            // Map from OrderItem to ReadOrderItemDto
            CreateMap<CartItem, OrderItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
           .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
           .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
           .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Product.Price))
           .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Quantity * src.Product.Price));



            CreateMap<OrderItem, ReadOrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Quantity * src.Product.Price));



            // CreateMap<OrderItem, AddOrderItemDto>().ReverseMap();
            //            CreateMap<OrderItem, UpdateOrderItemDto>().ReverseMap()
            //                    .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.ProductName))
            //;
            #endregion

            #region Review Mappings
            CreateMap<Review, ReadReviewDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName));

            CreateMap<Review, UpdateReviewDto>().ReverseMap();
            CreateMap<Review, AddReviewDto>().ReverseMap();
            #endregion


            CreateMap<CartItem, ReadCartItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id));

            CreateMap<CartItem, AddCartItemDto>().ReverseMap();
            CreateMap<CartItem, UpdateCartItemDto>().ReverseMap();



            CreateMap<Cart, ReadCartDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id));

            CreateMap<Cart, AddCartDto>();

            #region Payment Mappings
            // Mapping from AddPaymentDto to Payment entity
            CreateMap<AddPaymentDto, Payment>()
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency))
                // .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.Status, opt => opt.Ignore()) // Status will be set after payment intent creation
                .ForMember(dest => dest.PaymentIntentId, opt => opt.Ignore()) // PaymentIntentId comes from Stripe response
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Set manually
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()); // Set manually

            CreateMap<Cart, UpdateCartDto>().ReverseMap()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.Now));

            CreateMap<Cart, AddCartDto>().ReverseMap()
               .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.Now));
            // Mapping from PaymentIntent (Stripe response) to AddIntentPaymentResponseDto
            CreateMap<PaymentIntent, AddIntentPymentRsponsDto>()
                .ForMember(dest => dest.PaymentIntentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            // Mapping from Payment entity to ReadPaymentDto (optional for reading data)
            CreateMap<Payment, ReadPaymentDto>()
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.PaymentIntentId, opt => opt.MapFrom(src => src.PaymentIntentId))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));
            #endregion

            #region Shipping Mappings
            // Mapping from Shipping to ReadShippingDto
            CreateMap<Shipping, ReadShippingDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
                  .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName)).ReverseMap();

            // Mapping from AddShippingDto to Shipping
            CreateMap<AddShippingDto, Shipping>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId)) // Set UserId in controller
                .ForMember(dest => dest.ShippingStatus, opt => opt.MapFrom(_ => "Pending")) // Default status
                                                                                            //.ForMember(dest => dest.ShippedDate, opt => opt.MapFrom(_=>DateTime.Now)) 
                                                                                            //.ForMember(dest => dest.ExpectedDeliveryDate, opt => opt.MapFrom(_ => DateTime.Now.AddDays(10)))
                                                                                            //.ForMember(dest => dest.TrackingNumber, opt => opt.MapFrom(_ => Guid.NewGuid().ToString()))
                                                                                            //.ForMember(dest => dest.ShippingCost, opt => opt.MapFrom(_ => 60m))

                .ReverseMap();

            // Mapping from UpdateShippingDto to Shipping
            CreateMap<UpdateShippingDto, Shipping>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Id is managed in the controller
            #endregion

            #region Wishlist Mappings
            // Mapping for WishList
            CreateMap<WishList, ReadWishlistDto>()
                .ForMember(dest => dest.WishListItems, opt => opt.MapFrom(src => src.WishListItems));

            CreateMap<AddWishlistDto, WishList>();
            CreateMap<UpdateWishlistDto, WishList>();
            #endregion

            #region WishListItems Mappings
            // Mapping for WishListItems
            CreateMap<WishListItems, ReadwishlistItemsDto>();
            CreateMap<AddWishlistItemsDto, WishListItems>();
            CreateMap<UpdateWishlistItemsDto, WishListItems>();
            #endregion

            #region Category Mappings
            // Mapping from Category to ReadCategoryDto
            CreateMap<Category, ReadCategoryDto>();

            // Mapping from AddCategoryDTO to Category
            CreateMap<AddCategoryDTO, Category>();

            // Mapping from UpdateCategoryDto to Category
            CreateMap<UpdateCategoryDto, Category>();
            #endregion

            #region Inventory Mappings
            // Mapping from Inventory to ReadInventoryDto
            CreateMap<Inventory, ReadInventoryDto>();

            // Mapping from AddInventoryDto to Inventory
            CreateMap<AddInventoryDto, Inventory>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow)) // Set created date
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()); // Ignore updated date on creation

            // Mapping from UpdateInventoryDto to Inventory
            CreateMap<UpdateInventoryDto, Inventory>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Id is managed in the controller
                .ForMember(dest => dest.Product, opt => opt.Ignore()); // Ignore navigation property during update
            #endregion
        }
    }
}
