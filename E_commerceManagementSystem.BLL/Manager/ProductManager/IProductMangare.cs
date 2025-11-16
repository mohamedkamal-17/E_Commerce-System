using E_commerceManagementSystem.BLL.Dto.ProductDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;

namespace E_commerceManagementSystem.BLL.Manager.ProductManager
{
    public interface IProductMangare : IManager<Product, ReadProductDto, AddProductDto, UpdateProductDto>
    {
        Task<GeneralRespons> GetAllProducts();
        Task<GeneralRespons> GetByCategoryNameAsync(string categoryName);
        Task<GeneralRespons> GetByProductNameAsync(string ProductName);
        Task<GeneralRespons> GetByPriceAsync(float price);
        Task<GeneralRespons> GetByPriceInRangeAsync(float highPrice, float lowPrice);
        Task<GeneralRespons> GetByPriceLessThanAsync(float highPrice);
        Task<GeneralRespons> GetByPriceGreaterThanAsync(float lowPrice);
    }
}
