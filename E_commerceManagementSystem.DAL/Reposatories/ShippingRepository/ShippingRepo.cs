using E_commerceManagementSystem.DAL.Data.Dphelper;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.GeneralRepository;

namespace E_commerceManagementSystem.DAL.Reposatories.ShippingRepository
{
    public class ShippingRepo : Repository<Shipping>, IShippingRepo
    {
        private readonly ApplicationDbContext context;

        public ShippingRepo(ApplicationDbContext Context) : base(Context)
        {
            context = Context;
        }

        public ICollection<Shipping> GetByOrderIdAsync(int orderId)
        {
            return _context.Set<Shipping>().Where(sh => sh.OrderId == orderId).ToList();
        }


    }

}

