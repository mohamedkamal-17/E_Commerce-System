using E_commerceManagementSystem.DAL.Data.Dphelper;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.GeneralRepository;

namespace E_commerceManagementSystem.DAL.Reposatories.WishListItemsRepository
{
    public class WishListItemsRepo : Repository<WishListItems>, IWishListItemsRepo
    {
        private readonly ApplicationDbContext _context;

        public WishListItemsRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
