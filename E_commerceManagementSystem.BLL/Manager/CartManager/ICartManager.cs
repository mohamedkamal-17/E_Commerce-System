using E_commerceManagementSystem.BLL.Dto.CartDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;

namespace E_commerceManagementSystem.BLL.Manager.CartManager
{
    public interface ICartManager : IManager<Cart, ReadCartDto, AddCartDto, UpdateCartDto>
    {
        Task<GeneralRespons> GetByUserIdAsync(string userId);
        Task RemoveCartItems(IEnumerable<CartItem> cartItems);
        //  Task<GeneralRespons> UpdateCartItemsInCart(int id, List<UpdateCartItemsInCartDto> cartItemsInCart);
    }
}
