using E_commerceManagementSystem.DAL.Data.Dphelper;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.GeneralRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.DAL.Reposatories.OrederRepository
{
    public class OrederRepo : Repository<Order>, IOrederRepo
    {
        private readonly ApplicationDbContext _context;
        public OrederRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IQueryable<Order>> GetByUserIdAsync(string userId)
        {
            return _context.Orders.Where(u=>u.UserId== userId).AsNoTracking();
        }
    }
}
