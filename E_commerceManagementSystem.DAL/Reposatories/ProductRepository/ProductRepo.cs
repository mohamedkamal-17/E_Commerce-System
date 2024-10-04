using E_commerceManagementSystem.DAL.Data.Dphelper;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.GeneralRepository;
using E_commerceManagementSystem.DAL.Reposatories.ProductRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.DAL.Repositories.Classes
{
    public class ProductRepo : Repository<Product> ,IProductRepo
    {
        public ProductRepo(ApplicationDbContext Context) : base(Context)
        {

        }
        public Task<IEnumerable<Product>> GetByCategoryNameAsync()
        {
            throw new NotImplementedException();
        }
    }
}
