using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.CategoryDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.CategoryManger;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.CategoryRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.CategoryManager
{
    public class CategoryManager : Manager<Category, ReadCategoryDto, AddCategoryDTO, UpdateCategoryDto>, ICategoryManager
    {
        private readonly ICategoryRepo _repository;
        private readonly IMapper _mapper;

        public CategoryManager(ICategoryRepo repository, IMapper mapper)
            : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GeneralRespons> GetByCategoryNameAsync(string categoryName)
        {
            try
            {
                var category = await _repository.GetByConditionAsync(c => c.Name == categoryName)
                                                                    .FirstOrDefaultAsync();

                if (category == null)
                {
                    return CreateResponse(false, null, "No category found for the given name.", 404); // Not Found
                }

                var readDto = _mapper.Map<ReadCategoryDto>(category);
                return CreateResponse(true, readDto, "Category retrieved successfully by name.", 200); // OK
            }
            catch (Exception ex)
            {
                return CreateResponse(false, null, $"An error occurred while processing your request: {ex.Message}. Please try again later.", 500, new List<string> { ex.Message }); // Internal Server Error
            }
        }
    }
}
