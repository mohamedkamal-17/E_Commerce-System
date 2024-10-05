using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.ProductDto;
using E_commerceManagementSystem.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Product, AddproductDto>().ReverseMap();
            CreateMap<Product,UpdateProductDto>().ReverseMap();
         
        }

    }
}
