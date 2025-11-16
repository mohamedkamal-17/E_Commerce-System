using E_commerceManagementSystem.DAL.Data.Dphelper;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.GeneralRepository;

namespace E_commerceManagementSystem.DAL.Reposatories.WishlistRepsitory
{
    public class WishlistRepo : Repository<WishList>, IWishlistRepo
    {
        public WishlistRepo(ApplicationDbContext context) : base(context) { }
    }
}
