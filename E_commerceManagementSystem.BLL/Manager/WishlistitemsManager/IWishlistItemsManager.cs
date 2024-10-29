using E_commerceManagementSystem.BLL.Dto.WishlistDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;

namespace E_commerceManagementSystem.BLL.Manager.WishlistItemsManager
{
    public interface IWishlistItemsManager : IManager<WishListItems, ReadWishlistDto, AddWishlistDto, UpdateWishlistDto>
    {

    }
}
