using E_commerceManagementSystem.DAL.Data.Dphelper;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.GeneralRepository;

namespace E_commerceManagementSystem.DAL.Reposatories.InventoryRepository
{
    public class InventoryRepo : Repository<Inventory>, IInventoryRepo
    {
        public InventoryRepo(ApplicationDbContext Context) : base(Context)
        {

        }

    }
}
