using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.OrderDto;
using E_commerceManagementSystem.BLL.Dto.OrederItemDto;
using E_commerceManagementSystem.BLL.Dto.ProductDto;
using E_commerceManagementSystem.BLL.Dto.ReviewDto;
using E_commerceManagementSystem.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Product, AddProductDto>().ReverseMap();
            CreateMap<Product,UpdateProductDto>().ReverseMap();

            CreateMap<Order, ReadOrderDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.OrderItems.Sum(ci => ci.Quantity * ci.Product.Price)));
            CreateMap<Order, AddOrderDto>().ReverseMap();
            CreateMap<Order, UpdateOrderDto>().ReverseMap();

            CreateMap<OrderItem, ReadOrderItemDto>()
                 .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                 .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Quantity * src.Product.Price));
            CreateMap<OrderItem, AddOrderItemDto>().ReverseMap();
            CreateMap<OrderItem, UpdateOrderItemDto>().ReverseMap();


            CreateMap<Review, ReadReviewDto>()
                  .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName));

            CreateMap<Review, UpdateReviewDto>().ReverseMap();
            CreateMap<Review, AddReviewDto>().ReverseMap();



        }


    }
}
