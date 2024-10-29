using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.ReviewDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.ProductRepository;
using E_commerceManagementSystem.DAL.Reposatories.ReviewRepository;
using Microsoft.AspNetCore.Identity;

namespace E_commerceManagementSystem.BLL.Manager.ReviewManager
{
    public class ReviewManager : Manager<Review, ReadReviewDto, AddReviewDto, UpdateReviewDto>, IReviewManager
    {
        private readonly IReviewRepo _reviewRepo;
        private readonly IMapper _mapper;
        private readonly IProductRepo _productRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewManager(IReviewRepo repository, IMapper mapper, IProductRepo productRepo, UserManager<ApplicationUser> userManager)
            : base(repository, mapper)
        {
            _reviewRepo = repository;
            _mapper = mapper;
            _productRepo = productRepo;
            _userManager = userManager;
        }


        public override async Task<GeneralRespons> GetAllAsync()
        {
            return await base.GetAll(ve => ve.User, Version => Version.Product);


        }
        public override async Task<GeneralRespons> GetByIdAsync(int id)
        {
            return await base.GetAllByConditionAndIncludes(ve => ve.Id == id, ve => ve.User, Version => Version.Product);



        }

        public async Task<GeneralRespons> GetByProductIdAsync(int productId)
        {

            // Check if the product exists
            return await base.GetAllByConditionAndIncludes(ve => ve.ProductId == productId, ve => ve.User, Version => Version.Product);


        }

        public async Task<GeneralRespons> GetByUserIdAsync(string userId)
        {

            // Check if the user exists
            return await base.GetAllByConditionAndIncludes(ve => ve.UserId == userId, ve => ve.User, Version => Version.Product);
        }
    }
}
