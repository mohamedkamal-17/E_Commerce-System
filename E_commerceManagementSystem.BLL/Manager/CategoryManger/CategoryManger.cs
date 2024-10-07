using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.CategoryDto;
using E_commerceManagementSystem.BLL.Dto.ProductDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.CategoryRepository;
using E_commerceManagementSystem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.CategoryManger
{
    public class CategoryManger:Manager<Category, ReadCategoryDto, AddCategoryDTO, UpdateCategoryDto>, ICategoryManger
      
    {
        private readonly ICategoryRepo _repository;
        private readonly IMapper _mapper;

        public CategoryManger(ICategoryRepo repository,IMapper mapper):base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private GeneralRespons CreateResponse(bool success, object? model, string message, List<string>? errors = null)
        {
            return new GeneralRespons
            {
                Success = success,
                Model = model,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }
        public async Task< GeneralRespons> GetByCategoryNameAsync(string categoryName )
        {
            var queryableResult = await _repository.GetAllAsync();
            var categoriesLisr=await queryableResult.Where(co=>co.Name== categoryName).ToListAsync();
            var Categor= categoriesLisr.FirstOrDefault();
            if (Categor ==null)
            {
                ReadCategoryDto readDtos = _mapper.Map<ReadCategoryDto>(Categor);

                return CreateResponse(true, readDtos, "Category retrieved successfully by price.");
            }
            return CreateResponse(false, null, "No Category found for the given price.");



        }
    }
}
