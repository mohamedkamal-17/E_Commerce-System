using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Repositories.Interfaces;

namespace E_commerceManagementSystem.DAL.Reposatories.ShippingRepository
{
    public interface IShippingRepo : IRepository<Shipping>
    {
        public ICollection<Shipping> GetByOrderIdAsync(int id);
    }
}
