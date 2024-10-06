using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.CategoryDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.CategoryRepository;
using E_commerceManagementSystem.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.CategoryManger
{
    public class CategoryManger:Manager<Category, AddCategoryDTO, UpdateCategoryDto>, ICategoryManger
      
    {
        public CategoryManger(IRepository<Category> repository,IMapper mapper):base(repository, mapper) { }
    }
}
