using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.CartDto;
using E_commerceManagementSystem.BLL.Dto.CartItemDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.CartItemRepository;
using E_commerceManagementSystem.DAL.Reposatories.CartRepository;
using E_commerceManagementSystem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.CartItemManager
{
    public class CartItemManager : Manager<CartItem, ReadCartItemDto, AddCartItemDto, UpdateCartItemDto> , ICartItemManager
    {
        private readonly ICartItemRepo _cartItemrepository;
        private readonly IMapper _mapper;
        private readonly ICartRepo _cartRepo;

        public CartItemManager(ICartItemRepo repository, IMapper mapper, ICartRepo cartRepo) : base(repository, mapper)
        {
            _cartItemrepository = repository;
            _mapper = mapper;
            _cartRepo = cartRepo;
        }

        public async Task<GeneralRespons> GetByCartIdAsync(int cartId)
        {
            var cartExists = await _cartRepo.GetByIdAsync(cartId);
            if (cartExists == null)
            {
                return CreateResponse(false, null, "No cart with this id.", 404); // Not Found
            }
            try
            {
                var cartItems = await _cartItemrepository.GetByConditionAsync(c => c.CartID == cartId).ToListAsync();
                //var cartItems = await _cartItemrepository.GetByCartIdAsync(cartId);
                if (cartItems == null)
                {
                    return CreateResponse(false, null, "this cart has not any items", 404); // Not Found
                }

                var readDto = _mapper.Map<ICollection<ReadCartItemDto>>(cartItems);
                return CreateResponse(true, readDto, "cart retrieved successfully", 200); // OK

            }
            catch (Exception ex)
            {
                return CreateResponse(false, null, $"An error occurred while processing your request: {ex.Message}. Please try again later.", 500, new List<string> { ex.Message }); // Internal Server Error
            }
        }

    }
}
