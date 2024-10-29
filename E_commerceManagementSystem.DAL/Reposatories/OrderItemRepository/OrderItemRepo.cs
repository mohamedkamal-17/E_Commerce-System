using E_commerceManagementSystem.DAL.Data.Dphelper;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.GeneralRepository;

namespace E_commerceManagementSystem.DAL.Reposatories.OrederItemRepository
{
    public class OrderItemRepo : Repository<OrderItem>, IOrderItemRepo
    {
        private readonly ApplicationDbContext _context;
        public OrderItemRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


    }
}
