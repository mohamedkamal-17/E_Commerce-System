using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.CategoryDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.CategoryManger;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.CategoryRepository;

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

            return await base.GetAll(c => c.Name == categoryName);


        }
    }
}
