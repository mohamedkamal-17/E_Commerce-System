using E_commerceManagementSystem.DAL.Data.Dphelper;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.GeneralRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.DAL.Reposatories.WishListItemsRepository
{
    public class WishListItemsRepo:Repository<WishListItems>,IWishListItemsRepo
    {
        private readonly ApplicationDbContext _context;

        public WishListItemsRepo(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
    }
}
