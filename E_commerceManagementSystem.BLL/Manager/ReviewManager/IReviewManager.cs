using E_commerceManagementSystem.BLL.Dto.ReviewDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;

namespace E_commerceManagementSystem.BLL.Manager.ReviewManager
{
    public interface IReviewManager : IManager<Review, ReadReviewDto, AddReviewDto, UpdateReviewDto>
    {
        Task<GeneralRespons> GetByUserIdAsync(string userId);
        Task<GeneralRespons> GetByProductIdAsync(int productid);
    }
}
