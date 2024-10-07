using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.ProductDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.GeneralRepository;
using E_commerceManagementSystem.DAL.Reposatories.ProductRepository;
using E_commerceManagementSystem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.ProductManager
{
    public class ProductManger : Manager<Product, ReadProductDto, AddproductDto, UpdateProductDto>, IProductMangare
    {
        private readonly IProductRepo _repository;
        private readonly IMapper _mapper;

        public ProductManger(IProductRepo repository, IMapper mapper)
             : base(repository, mapper)
        {
           _repository = repository;
            _mapper = mapper;
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


        public async Task<GeneralRespons> GetByPraiceAsync(float price)
        {
            try
            {
                var products = await _repository.GetAllAsync();
                var resultLoist = await products.Where(p => p.Price == price).ToListAsync();

                if (resultLoist != null && resultLoist.Count > 0)
                {
                   List<ReadProductDto> readDtos= _mapper.Map<List<ReadProductDto>>(resultLoist);

                    return CreateResponse(true, readDtos, "Products retrieved successfully by price.");
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
                var resultList = await products.Where(p => p.Price >= lowPrice && p.Price <= highPrice).ToListAsync();

                if (resultList != null && resultList.Count > 0)
                {
                    List<ReadProductDto> readDtos = _mapper.Map<List<ReadProductDto>>(resultList);
                    return CreateResponse(true, readDtos, "Products retrieved successfully within the price range.");
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
                var resultList = products.Where(p => p.Price > lowPrice).ToList();

                if (resultList != null && resultList.Count > 0)
                {
                    List<ReadProductDto> readDtos = _mapper.Map<List<ReadProductDto>>(resultList);
                    return CreateResponse(true, readDtos, "Products retrieved successfully with price greater than specified.");
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
                var resultList = await products.Where(p => p.Price < highPrice).ToListAsync();

                if (resultList != null && resultList.Count > 0)
                {
                    List<ReadProductDto> readDtos = _mapper.Map<List<ReadProductDto>>(resultList);

                    return CreateResponse(true, readDtos, "Products retrieved successfully with price less than specified.");
                }
                return CreateResponse(false, null, "No products found with price less than specified.");
            }
            catch (Exception ex)
            {
                return CreateResponse(false, null, $"Error retrieving products: {ex.Message}", new List<string> { ex.Message });
            }
        }




        async Task<GeneralRespons> IProductMangare.GetByCategoryNameAsync(string categoryName)
        {
            try
            {
                var products = await _repository.GetAllAsync(); // Assume GetAllAsync returns IQueryable<Product>
                var resultList = products.Where(p => p.Category.Name == categoryName).ToList();

                if (resultList != null && resultList.Count > 0)
                {
                    List<ReadProductDto> readDtos = _mapper.Map<List<ReadProductDto>>(resultList);
                    return CreateResponse(true, readDtos, "Products retrieved successfully by category.");
                }
                return CreateResponse(false, null, "No products found for the given category.");
            }
            catch (Exception ex)
            {
                return CreateResponse(false, null, $"Error retrieving products: {ex.Message}", new List<string> { ex.Message });
            }

        }

        public async Task<GeneralRespons> GetByProductNameAsync(string ProductName)
        {
            try
            {
                var products = await _repository.GetAllAsync(); // Assume GetAllAsync returns IQueryable<Product>
                var product = products.FirstOrDefault(p => p.ProductName == ProductName);
              
                if (product != null)
                {
                    ReadProductDto readDtos = _mapper.Map<ReadProductDto>(product);
                    return CreateResponse(true, readDtos, "Products retrieved successfully by category.");
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
