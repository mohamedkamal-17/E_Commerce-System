using E_commerceManagementSystem.DAL.Data.Dphelper;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.GeneralRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.DAL.Reposatories.CartItemRepository
{
    public class CartItemRepo : Repository<CartItem> , ICartItemRepo
    {
        private readonly ApplicationDbContext _context;

        public CartItemRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IQueryable<CartItem>> GetByCartIdAsync(int cartId)
        {
            return _context.CartItems.Where(x => x.CartID == cartId).AsNoTracking();
        }
    }
}
