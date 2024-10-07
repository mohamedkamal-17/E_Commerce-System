using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.CartDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.CartRepository;
using E_commerceManagementSystem.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.CartManager
{
    public class CartManager : Manager<Cart, ReadCartDto, AddCartDto, UpdateCartDto>, ICartManager
    {
        private readonly ICartRepo _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartManager(ICartRepo repository, IMapper mapper, UserManager<ApplicationUser> userManager) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
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

            var cart = await _repository.GetByUserIdAsync(userId);
            if(cart == null)
            {
                return new GeneralRespons
                {
                    Success = false,
                    Message = "user has not cart"
                };
            }
            
            
            return new GeneralRespons
            {
                Success = true,
                Model = _mapper.Map<ReadCartDto>(cart),
            };
        }
    }
}
