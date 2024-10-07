using E_commerceManagementSystem.DAL.Data.Dphelper;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.GeneralRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.DAL.Reposatories.OrederItemRepository
{
    public class OrderItemRepo : Repository<OrderItem> , IOrderItemRepo
    {
        private readonly ApplicationDbContext _context;
        public OrderItemRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IQueryable<OrderItem>> GetByOrderIdAsync(int orderId)
        {
            return _context.OrderItems.AsNoTracking();
        }
    }
}
