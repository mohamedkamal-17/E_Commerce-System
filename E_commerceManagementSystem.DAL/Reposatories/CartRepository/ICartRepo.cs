using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Repositories.Interfaces;

namespace E_commerceManagementSystem.DAL.Reposatories.CartRepository
{
    public interface ICartRepo : IRepository<Cart>
    {
        Task RemoveCartItemsAsync(IEnumerable<CartItem> cartItems);
    }


}
