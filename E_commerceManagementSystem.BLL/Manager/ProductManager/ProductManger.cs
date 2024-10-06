using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.ProductDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.GeneralRepository;
using E_commerceManagementSystem.DAL.Reposatories.ProductRepository;
using E_commerceManagementSystem.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.ProductManager
{
    public class ProductManger : Manager<Product, AddproductDto, UpdateProductDto>, IProductMangare
    {
        private readonly IProductRepo _repository;

        public ProductManger(IProductRepo repository, IMapper mapper)
             : base(repository, mapper)
        {
           _repository = repository;
        }
        private GeneralRespons CreateResponse(bool success, object? model, string message, List<string>? errors = null)
        {
            return new GeneralRespons
            {
                Success = success,
                Model = model,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }

        public Task<GeneralRespons> GetByCategoryNameAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<GeneralRespons> GetByPraiceAsync(float price)
        {
            try
            {
                var products = await _repository.GetAllAsync();
                var result = products.Where(p => p.Price == price).ToList();

                if (result.Count > 0)
                {
                    return CreateResponse(true, result, "Products retrieved successfully by price.");
                }
                return CreateResponse(false, null, "No products found for the given price.");
            }
            catch (Exception ex)
            {
                return CreateResponse(false, null, $"Error retrieving products: {ex.Message}", new List<string> { ex.Message });
            }
        }


        public async Task<GeneralRespons> GetByPraiceInRangeAsync(float highPrice, float lowPrice)
        {
            try
            {
                var products = await _repository.GetAllAsync();
                var result = products.Where(p => p.Price >= lowPrice && p.Price <= highPrice).ToList();

                if (result.Count > 0)
                {
                    return CreateResponse(true, result, "Products retrieved successfully within the price range.");
                }
                return CreateResponse(false, null, "No products found within the price range.");
            }
            catch (Exception ex)
            {
                return CreateResponse(false, null, $"Error retrieving products: {ex.Message}", new List<string> { ex.Message });
            }
        }


        public async Task<GeneralRespons> GetByPraicelargthanAsync(float lowPrice)
        {
            try
            {
                var products = await _repository.GetAllAsync();
                var result = products.Where(p => p.Price > lowPrice).ToList();

                if (result.Count > 0)
                {
                    return CreateResponse(true, result, "Products retrieved successfully with price greater than specified.");
                }
                return CreateResponse(false, null, "No products found with price greater than specified.");
            }
            catch (Exception ex)
            {
                return CreateResponse(false, null, $"Error retrieving products: {ex.Message}", new List<string> { ex.Message });
            }
        }


        public async Task<GeneralRespons> GetByPraicelessthanAsync(float highPrice)
        {
            try
            {
                var products = await _repository.GetAllAsync();
                var result = products.Where(p => p.Price < highPrice).ToList();

                if (result.Count > 0)
                {
                    return CreateResponse(true, result, "Products retrieved successfully with price less than specified.");
                }
                return CreateResponse(false, null, "No products found with price less than specified.");
            }
            catch (Exception ex)
            {
                return CreateResponse(false, null, $"Error retrieving products: {ex.Message}", new List<string> { ex.Message });
            }
        }




        async Task<GeneralRespons> IProductMangare.GetProductByCategoryNameAsync(string categoryName)
        {
            try
            {
                var products = await _repository.GetAllAsync(); // Assume GetAllAsync returns IQueryable<Product>
                var result = products.Where(p => p.Category.Name == categoryName).ToList();
              
                if (result.Count > 0)
                {
                    return CreateResponse(true, result, "Products retrieved successfully by category.");
                }
                return CreateResponse(false, null, "No products found for the given category.");
            }
            catch (Exception ex)
            {
                return CreateResponse(false, null, $"Error retrieving products: {ex.Message}", new List<string> { ex.Message });
            }

        }
    }
}
