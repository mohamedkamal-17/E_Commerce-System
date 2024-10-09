using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.CartItemDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.CartItemRepository;
using E_commerceManagementSystem.DAL.Reposatories.CartRepository;
using E_commerceManagementSystem.DAL.Repositories.Interfaces;
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
                return new GeneralRespons
                {
                    Success = false,
                    Message = "no cart with this id"
                };
            }

            var cartItems = await _cartItemrepository.GetByCartIdAsync(cartId);
            if (cartItems == null)
            {
                return new GeneralRespons
                {
                    Success = false,
                    Message = "this cart has not any items yet"
                };
            }

            var cartItemDto = _mapper.Map<ICollection<ReadCartItemDto>>(cartItems);
            return new GeneralRespons
            {
                Success = true,
                Model = cartItemDto
            };
        }
    }
}
