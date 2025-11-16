using E_commerceManagementSystem.BLL.Dto.WishlistDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;

namespace E_commerceManagementSystem.BLL.Manager.WishlistManager
{
    public interface IWishlistManager : IManager<WishList, ReadWishlistDto, AddWishlistDto, UpdateWishlistDto>
    {

        Task<GeneralRespons> GetByUserID(string userId);
    }
}
