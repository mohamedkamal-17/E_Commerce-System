using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.ReviewDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.BLL.Manager.ProductManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.ProductRepository;
using E_commerceManagementSystem.DAL.Reposatories.ReviewRepository;
using E_commerceManagementSystem.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.ReviewManager
{
    public class ReviewManager : Manager<Review, AddReviewDto, UpdateReviewDto>, IReviewManager
    {
        private readonly IReviewRepo _reviewRepo;
        private readonly IProductRepo _productRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        public ReviewManager(IReviewRepo repository, IMapper mapper, IProductRepo productRepo, UserManager<ApplicationUser> userManager)
            : base(repository, mapper)
        {
            _reviewRepo = repository;
            _productRepo = productRepo;
            _userManager = userManager;
        }

        public async Task<GeneralRespons> GetByProductIdAsync(int productid)
        {
            var productExists = await _productRepo.GetByIdAsync(productid);
            if(productExists == null)
            {
                return new GeneralRespons
                {
                    Success = false,
                    Message = "No Product with this id"
                };
            }
            var review = await _reviewRepo.GetByProductIdAsync(productid);
            if(await review.CountAsync() == 0)
            {
                return new GeneralRespons
                {
                    Success = false,
                    Message = "No reviews for this product"
                };
            }
            var reviews = await review.ToListAsync();

            var reviewDtos = reviews.Select(r => new ReadReviewDto
            {
                ProductName = r.Product.ProductName,
                UserName = r.User.UserName,
                Rating = r.Rating,
                ReviewText = r.ReviewText,
                CreatedDate = r.CreatedDate
            }).ToList();

            return new GeneralRespons
            {
                Success = true,
                Model = reviewDtos
            };
        }

        public async Task<GeneralRespons> GetByUserIdAsync(string userId)
        {
            var userExists = await _userManager.FindByIdAsync(userId);
            if (userExists == null)
            {
                return new GeneralRespons
                {
                    Success = false,
                    Message = "No user with this id"
                };
            }
            var review = await _reviewRepo.GetByUserIdAsync(userId);
            if (await review.CountAsync() == 0)
            {
                return new GeneralRespons
                {
                    Success = false,
                    Message = "this user have not any reviews"
                };
            }
            var reviews = await review.ToListAsync();

            var reviewDtos = reviews.Select(r => new ReadReviewDto
            {
                ProductName = r.Product.ProductName,
                UserName = r.User.UserName,
                Rating = r.Rating,
                ReviewText = r.ReviewText,
                CreatedDate = r.CreatedDate
            }).ToList();

            return new GeneralRespons
            {
                Success = true,
                Model = reviewDtos
            };
        }
    }
}
