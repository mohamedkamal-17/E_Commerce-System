using AutoMapper;
using AutoMapper.QueryableExtensions; // Import for ProjectTo
using E_commerceManagementSystem.BLL.Dto.ProductDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.ProductRepository;
using E_commerceManagementSystem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.ProductManager
{
    public class ProductManager : Manager<Product, ReadProductDto, AddProductDto, UpdateProductDto>, IProductMangare
    {
        private readonly IProductRepo _repository;
        private readonly IMapper _mapper;

        public ProductManager(IProductRepo repository, IMapper mapper)
            : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GeneralRespons> GetAllProducts()
        {
            return await base.GetAll( p => p.Category);

        }
        public async Task<GeneralRespons> GetProductbyId(int id)
        {
            return await base.GetAll(p => p.Id==id);

        }
        public async Task<GeneralRespons> GetByPriceAsync(float price)
        {
            return await base.GetAllByConditionAndIncludes(p => p.Price == price,p=>p.Category);
           
        }

        public async Task<GeneralRespons> GetByPriceInRangeAsync(float highPrice, float lowPrice)
        {
            return await base.GetAllByConditionAndIncludes(p => p.Price >= lowPrice && p.Price <= highPrice, p => p.Category);
           
        }

        public async Task<GeneralRespons> GetByPriceGreaterThanAsync(float lowPrice)
        {
            return await base.GetAllByConditionAndIncludes(p => p.Price > lowPrice, p => p.Category);
           
        }

        public async Task<GeneralRespons> GetByPriceLessThanAsync(float highPrice)
        {
            return await base.GetAll(p => p.Price < highPrice, p => p.Category);
           
        }

        

        public async Task<GeneralRespons> GetByCategoryNameAsync(string categoryName)
        {
            return await base.GetAllByConditionAndIncludes(p => p.Category.Name == categoryName, p => p.Category);
            
        }

        public async Task<GeneralRespons> GetByProductNameAsync(string productName)
        {
            return await base.GetAllByConditionAndIncludes(
                p => p.ProductName == productName, p => p.Category);
        }
    }
}
