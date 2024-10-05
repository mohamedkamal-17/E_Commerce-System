﻿using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.ProductManager
{
    public interface IProductMangare : IManager<Product>
    {
        Task<IEnumerable<Product>> GetProductByCategoryNameAsync(string Name);
    }
}