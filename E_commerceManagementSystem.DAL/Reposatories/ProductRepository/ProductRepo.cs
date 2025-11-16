using E_commerceManagementSystem.DAL.Data.Dphelper;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.GeneralRepository;
using E_commerceManagementSystem.DAL.Reposatories.ProductRepository;

namespace E_commerceManagementSystem.DAL.Repositories.Classes
{
    public class ProductRepo : Repository<Product>, IProductRepo
    {
        public ProductRepo(ApplicationDbContext Context) : base(Context)
        {

        }

    }
}
