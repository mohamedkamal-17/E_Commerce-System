using E_commerceManagementSystem.DAL.Data.Dphelper;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.GeneralRepository;

namespace E_commerceManagementSystem.DAL.Reposatories.CartRepository
{
    public class CartRepo : Repository<Cart>, ICartRepo
    {
        private readonly ApplicationDbContext _context;
        public CartRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task RemoveCartItemsAsync(IEnumerable<CartItem> cartItems)
        {
            _context.CartItems.RemoveRange(cartItems);

        }
    }
}
