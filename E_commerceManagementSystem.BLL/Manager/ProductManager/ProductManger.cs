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

        // CreateResponse method to include status code
        private GeneralRespons CreateResponse(bool success, object? model, string message, int statusCode, List<string>? errors = null)
        {
            return new GeneralRespons
            {
                Success = success,
                Model = model,
                Message = message,
                Errors = errors ?? new List<string>(),
                StatusCode = statusCode // Add status code property
            };
        }

        public async Task<GeneralRespons> GetByPriceAsync(float price)
        {
            return await GetProductsByConditionAsync(
                p => p.Price == price,
                "Products retrieved successfully by price.",
                "No products found for the given price.",
                200, // OK
                404); // Not Found
        }

        public async Task<GeneralRespons> GetByPriceInRangeAsync(float highPrice, float lowPrice)
        {
            return await GetProductsByConditionAsync(
                p => p.Price >= lowPrice && p.Price <= highPrice,
                "Products retrieved successfully within the price range.",
                "No products found within the price range.",
                200, // OK
                404); // Not Found
        }

        public async Task<GeneralRespons> GetByPriceGreaterThanAsync(float lowPrice)
        {
            return await GetProductsByConditionAsync(
                p => p.Price > lowPrice,
                "Products retrieved successfully with price greater than specified.",
                "No products found with price greater than specified.",
                200, // OK
                404); // Not Found
        }

        public async Task<GeneralRespons> GetByPriceLessThanAsync(float highPrice)
        {
            return await GetProductsByConditionAsync(
                p => p.Price < highPrice,
                "Products retrieved successfully with price less than specified.",
                "No products found with price less than specified.",
                200, // OK
                404); // Not Found
        }

        private async Task<GeneralRespons> GetProductsByConditionAsync(Expression<Func<Product, bool>> filter,
            string successMessage, string failureMessage, int successStatusCode, int failureStatusCode)
        {
            try
            {
                // Using ProjectTo to map directly in the database query
                var readDtos = await _repository.GetByConditionAsync(filter)
                                                .ProjectTo<ReadProductDto>(_mapper.ConfigurationProvider)
                                                .ToListAsync();

                if (readDtos != null && readDtos.Count > 0)
                {
                    return CreateResponse(true, readDtos, successMessage, successStatusCode);
                }
                return CreateResponse(false, null, failureMessage, failureStatusCode);
            }
            catch (Exception ex)
            {
                return CreateResponse(false, null, $"Error retrieving products: {ex.Message}", 500, new List<string> { ex.Message }); // Internal Server Error
            }
        }

        public async Task<GeneralRespons> GetByCategoryNameAsync(string categoryName)
        {
            return await GetProductsByConditionAsync(
                p => p.Category.Name == categoryName,
                "Products retrieved successfully by category.",
                "No products found for the given category.",
                200, // OK
                404); // Not Found
        }

        public async Task<GeneralRespons> GetByProductNameAsync(string productName)
        {
            return await GetProductsByConditionAsync(
                p => p.ProductName == productName,
                "Product retrieved successfully by name.",
                "No products found for the given name.",
                200, // OK
                404); // Not Found
        }
    }
}
