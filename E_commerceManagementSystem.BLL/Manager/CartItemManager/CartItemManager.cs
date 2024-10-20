using AutoMapper;
using AutoMapper.QueryableExtensions;
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Intrinsics.Arm;
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
            return await base.GetAll(p => p.Product);
        }
        

        public async override Task<GeneralRespons> GetByIdAsync(int id)

        {
            // return await base.GetAllByConditionAndIncludes(e => e.Id == id, p => p.Product);
            
            var result = await _cartItemrepository.GetAll(e => e.Id == id, p => p.Product)
      .AsNoTracking()
      .ProjectTo<ReadCartItemDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
      
            if (result != null )
                return CreateResponse(true, result, $"{typeof(CartItem).Name}s retrieved successfully.", 200);

            return CreateResponse(false, null, $"{typeof(CartItem).Name} not found.", 404);



        }

        public async Task<GeneralRespons> GetByCartIdAsync(int cartId)
        {
            var cartExists =  _cartRepo.GetAll().Any(c => c.Id == cartId);
            if (!cartExists )
            {
                return CreateResponse(false, null, "No cart with this id.", 404); // Not Found
            }
            try
            {
                return await base.GetAllByConditionAndIncludes(c => c.CartID == cartId, p => p.Product);
            }
            catch (Exception ex)
            {
                return CreateResponse(false, null, $"An error occurred while processing your request: {ex.Message}. Please try again later.", 500, new List<string> { ex.Message }); // Internal Server Error
            }
        }

        public async Task<bool> CheckCartExistsAsync(int cartId)
        {
            return await _cartRepo.GetAll().AnyAsync(c => c.Id == cartId);
        }
        
        public async Task<bool> CheckProductExistsAsync(int productId)
        {
            return await _productRepo.GetAll().AnyAsync(p => p.Id == productId);
        }

        public async Task<GeneralRespons> ValidInput(int cartId, int productId)
        {

            var cartExists = await CheckCartExistsAsync(cartId);
            if (!cartExists)
            {
                return CreateResponse(false, null, "No cart with this id.", 404); // Not Found
            }

            var productExists = await CheckProductExistsAsync(productId);
            if (!productExists)
            {
                return CreateResponse(false, null, "No product with this id.", 404);
            }

           var result= await base.GetAllByConditionAndIncludes(c => c.CartID == cartId && c.Product.Id == productId, p => p.Product);

            if(result.Success)
            {
                return CreateResponse(false, null, "this Product Exist in this cart.", 400);

            }
            return CreateResponse(true, result.Model, "valid dto", 200);

        }
        //public async Task<GeneralRespons> GetByCartIdAndProductIdAsync(int cartId, int productId)
        //{
           

        //    try
        //    {

        //        return await base.GetAllByConditionAndIncludes(c => c.CartID == cartId && c.Product.Id == productId, p => p.Product);
        //        //var cartItemExists = await _cartItemrepository.GetByConditionAsync(c => c.CartID == cartId && c.Product.Id == productId)
        //        //    .Include(p => p.Product).FirstOrDefaultAsync();
        //        //if (cartItemExists != null)
        //        //{
        //        //    return CreateResponse(false, null, "this cart item already exists", 404);
        //        //}

        //        //var readDto = _mapper.Map<ReadCartItemDto>(cartItemExists);
        //        //return CreateResponse(true, readDto, "cart retrieved successfully", 200); // OK

        //    }
        //    catch (Exception ex)
        //    {
        //        return CreateResponse(false, null, $"An error occurred while processing your request: {ex.Message}. Please try again later.", 500, new List<string> { ex.Message }); // Internal Server Error
        //    }
        //}

    }
}
