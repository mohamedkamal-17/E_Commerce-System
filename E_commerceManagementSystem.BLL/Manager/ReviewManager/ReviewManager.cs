using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.ReviewDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.ReviewRepository;
using E_commerceManagementSystem.DAL.Repositories.Interfaces;
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
        public ReviewManager(IReviewRepo repository, IMapper mapper) 
            : base(repository, mapper)
        {
            _reviewRepo = repository;
        }

        public Task<GeneralRespons> GetByProductIdAsync(int productid)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralRespons> GetByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
