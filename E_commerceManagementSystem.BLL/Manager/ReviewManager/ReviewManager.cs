using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_commerceManagementSystem.BLL.Dto.ReviewDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.ProductRepository;
using E_commerceManagementSystem.DAL.Reposatories.ReviewRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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

      
        public async Task<GeneralRespons> GetByProductIdAsync(int productId)
        {
            try
            {
                // Check if the product exists
                var productExists = await _productRepo.GetByIdAsync(productId);
                if (productExists == null)
                {
                    return CreateResponse(false, null, "No Product with this ID", 404);
                }

                // Get reviews for the product
                var reviewDtos = await _reviewRepo.GetByConditionAsync(re => re.ProductId == productId)
                                                  .ProjectTo<ReadReviewDto>(_mapper.ConfigurationProvider)
                                                  .ToListAsync();

                if (!reviewDtos.Any())
                {
                    return CreateResponse(false, null, "No reviews for this product", 404);
                }

                return CreateResponse(true, reviewDtos, "Reviews retrieved successfully",200);
            }
            catch (Exception ex)
            {
                return CreateResponse(false, null, $"An error occurred while processing your request: {ex.Message}. Please try again later.", 500, new List<string> { ex.Message });
            }
        }

        public async Task<GeneralRespons> GetByUserIdAsync(string userId)
        {
            try
            {
                // Check if the user exists
                var userExists = await _userManager.FindByIdAsync(userId);
                if (userExists == null)
                {
                    return CreateResponse(false, null, "No user with this ID", 404);
                }

                // Retrieve reviews for the user
                var reviewDtos = await _reviewRepo.GetByConditionAsync(r => r.UserId == userId)
                                                  .ProjectTo<ReadReviewDto>(_mapper.ConfigurationProvider)
                                                  .ToListAsync();

                if (!reviewDtos.Any())
                {
                    return CreateResponse(false, null, "This user has not written any reviews", 404);
                }

                return CreateResponse(true, reviewDtos, "Reviews retrieved successfully", 200);
            }
            catch (Exception ex)
            {
                return CreateResponse(false, null, $"An error occurred while processing your request: {ex.Message}. Please try again later.", 500, new List<string> { ex.Message });
            }
        }
    }
}
