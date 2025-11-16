using E_commerceManagementSystem.BLL.Dto.CategoryDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;

namespace E_commerceManagementSystem.BLL.Manager.CategoryManger
{
    public interface ICategoryManager : IManager<Category, ReadCategoryDto, AddCategoryDTO, UpdateCategoryDto>
    {
        Task<GeneralRespons> GetByCategoryNameAsync(string categoryName);
    }
}
