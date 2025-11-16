using E_commerceManagementSystem.BLL.Dto.CartItemDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;

namespace E_commerceManagementSystem.BLL.Manager.CartItemManager
{
    public interface ICartItemManager : IManager<CartItem, ReadCartItemDto, AddCartItemDto, UpdateCartItemDto>
    {
        Task<GeneralRespons> GetByCartIdAsync(int cartId);
        //Task<GeneralRespons> GetByCartIdAndProductIdAsync(int cartId, int productId);
        Task<GeneralRespons> ValidInput(int cartId, int productId);
    }
}
