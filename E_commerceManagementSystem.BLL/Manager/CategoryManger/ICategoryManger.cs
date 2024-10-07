﻿using E_commerceManagementSystem.BLL.Dto.CategoryDto;
using E_commerceManagementSystem.BLL.Dto.ProductDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.CategoryManger
{
    public interface ICategoryManger: IManager<Category, ReadCategoryDto, AddCategoryDTO, UpdateCategoryDto>
    {
        Task<GeneralRespons> GetByCategoryNameAsync(string categoryName);
    }
}
