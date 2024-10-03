using E_commerceManagementSystem.BLL.Manager.Interfaces;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.Classes
{
    public class ProductManger : Manager<Product>, IProductMangare
    {
        ProductManger(IRepository<Product> repo) : base(repo) { }
        public Task<IEnumerable<Product>> GetPraoductByCategoryNameAsync(string Name)
        {
            throw new NotImplementedException();
        }
    }
}
