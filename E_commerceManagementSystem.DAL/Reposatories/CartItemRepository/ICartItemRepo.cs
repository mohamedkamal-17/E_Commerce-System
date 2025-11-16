using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Repositories.Interfaces;

namespace E_commerceManagementSystem.DAL.Reposatories.CartItemRepository
{
    public interface ICartItemRepo : IRepository<CartItem>
    {
        //Task<IQueryable<CartItem>> GetByCartIdAsync(int cartId);
    }
}
