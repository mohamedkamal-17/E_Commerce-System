using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.ProductDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.ProductManager
{
    public class ProductManger : Manager<Product, AddproductDto, UpdateProductDto>, IProductMangare
    {
        public ProductManger(IRepository<Product> repository, IMapper mapper)
             : base(repository, mapper)
        {
        }
        public Task<ICollection<Product>> GetProductByCategoryNameAsync(string Name)
        {
            throw new NotImplementedException();
        }




    }
}
