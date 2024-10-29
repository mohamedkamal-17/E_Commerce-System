using E_commerceManagementSystem.DAL.Data.Dphelper;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.GeneralRepository;

namespace E_commerceManagementSystem.DAL.Reposatories.CartItemRepository
{
    public class CartItemRepo : Repository<CartItem>, ICartItemRepo
    {
        private readonly ApplicationDbContext _context;

        public CartItemRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


    }
}
