using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Repositories.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.DAL.Repositories.Interfaces
{
    public interface IProductRepo: IRepository<Product>
    {
        Task<IEnumerable<Product>> GetByCategoryNameAsync();

    }
}
