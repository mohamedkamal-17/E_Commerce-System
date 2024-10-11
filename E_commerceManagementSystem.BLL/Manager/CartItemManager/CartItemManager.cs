using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.CartDto;
using E_commerceManagementSystem.BLL.Dto.CartItemDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.CartItemRepository;
using E_commerceManagementSystem.DAL.Reposatories.CartRepository;
using E_commerceManagementSystem.DAL.Reposatories.GeneralRepository;
using E_commerceManagementSystem.DAL.Reposatories.ProductRepository;
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
        private readonly IProductRepo _productRepo;
        public CartItemManager(ICartItemRepo repository, IMapper mapper, ICartRepo cartRepo, IProductRepo productRepo) : base(repository, mapper)
        {
            _cartItemrepository = repository;
            _mapper = mapper;
            _cartRepo = cartRepo;
            _productRepo = productRepo;
        }
        public async override Task<GeneralRespons> GetAllAsync()
        {
            // Include the navigation properties you need
            var queryableResult = await _cartItemrepository.GetAllWithIncludesAsync(u => u.Product);

            // Execute the query and get the result with product 
            var resultList = await queryableResult.Include(p=>p.Product).ToListAsync();

            if (resultList != null && resultList.Count > 0)
            {
                var dtoList = _mapper.Map<List<ReadCartItemDto>>(resultList);
                return CreateResponse(true, dtoList, "Cart Items retrieved successfully.", 200);
            }

            return CreateResponse(false, null, "Cart Items not found.", 404);
        }
        

        public async override Task<GeneralRespons> GetByIdAsync(int id)
        {
            var queryableResult = await _cartItemrepository.GetAllWithIncludesAsync(p=>p.Product);
            var result = await queryableResult
                .SingleOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);

            if (result != null)
            {
                var dto = _mapper.Map<ReadCartItemDto>(result);
                return CreateResponse(true, dto, $"{typeof(Cart).Name} retrieved successfully.", 200);
            }

            return CreateResponse(false, null, $"no cart item with this id.", 404);
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
                var cartItems = await _cartItemrepository.GetByConditionAsync(c => c.CartID == cartId)
                    .Include(p=>p.Product).ToListAsync();
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

        public async Task<GeneralRespons> GetByCartIdAndProductIdAsync(int cartId, int productId)
        {
            var cartExists = await GetByCartIdAsync(cartId);
            if (!cartExists.Success)
            {
                return cartExists;
            }

            var productExists = await _productRepo.GetByIdAsync(productId);
            if (productExists == null)
            {
                return CreateResponse(false, null, "No cart with this id.", 404); // Not Found
            }

            try
            {
                var cartItemExists = await _cartItemrepository.GetByConditionAsync(c => c.CartID == cartId && c.Product.Id == productId)
                    .Include(p => p.Product).FirstOrDefaultAsync();
                if (cartItemExists != null)
                {
                    return CreateResponse(false, null, "this cart item already exists", 404);
                }

                var readDto = _mapper.Map<ReadCartItemDto>(cartItemExists);
                return CreateResponse(true, readDto, "cart retrieved successfully", 200); // OK

            }
            catch (Exception ex)
            {
                return CreateResponse(false, null, $"An error occurred while processing your request: {ex.Message}. Please try again later.", 500, new List<string> { ex.Message }); // Internal Server Error
            }
        }

    }
}
