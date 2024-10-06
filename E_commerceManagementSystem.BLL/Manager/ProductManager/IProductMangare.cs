using E_commerceManagementSystem.BLL.Dto.ProductDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.ProductManager
{
    public interface IProductMangare : IManager<Product,AddproductDto,UpdateProductDto>
    {
        Task<GeneralRespons> GetProductByCategoryNameAsync(string Name);
        Task<GeneralRespons> GetByCategoryNameAsync();
        Task<GeneralRespons> GetByPraiceAsync(float price);
        Task<GeneralRespons> GetByPraiceInRangeAsync(float highPrice, float lowPrice);
        Task<GeneralRespons> GetByPraicelessthanAsync(float highPrice);
        Task<GeneralRespons> GetByPraicelargthanAsync(float lowPrice);
    }
}
