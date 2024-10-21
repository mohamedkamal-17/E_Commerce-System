using E_commerceManagementSystem.DAL.Data.Dphelper;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.GeneralRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.DAL.Reposatories.CartRepository
{
    public class CartRepo : Repository<Cart> , ICartRepo
    {
        private readonly ApplicationDbContext _context;
        public CartRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task RemoveCartItemsAsync(IEnumerable<CartItem> cartItems)
        {
            _context.CartItems.RemoveRange(cartItems); 
           // await _context.SaveChangesAsync(); 
        }
    }
}
