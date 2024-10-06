using E_commerceManagementSystem.BLL.Dto.ReviewDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.ReviewManager
{
    public interface IReviewManager : IManager<Review,AddReviewDto,UpdateReviewDto>
    {
        Task<GeneralRespons> GetByUserIdAsync(string userId);
        Task<GeneralRespons> GetByProductIdAsync(int productid);
    }
}
