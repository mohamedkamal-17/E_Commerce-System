using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.CartDto;
using E_commerceManagementSystem.BLL.Dto.CategoryDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.CartItemRepository;
using E_commerceManagementSystem.DAL.Reposatories.CartRepository;
using E_commerceManagementSystem.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.CartManager
{
    public class CartManager : Manager<Cart, ReadCartDto, AddCartDto, UpdateCartDto>, ICartManager
    {
        private readonly ICartRepo _repository;
        private readonly ICartItemRepo _cartItemRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartManager(ICartRepo repository, IMapper mapper, UserManager<ApplicationUser> userManager, ICartItemRepo cartItemRepo) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
            _cartItemRepo = cartItemRepo;
        }

        public override async Task<GeneralRespons> GetAllAsync()
        {
          //  return await base.GetAll(u => u.User, c => c.CartItems, c => c.CartItems.Select(p => p.Product));
          //  Include the navigation properties you need
            var resultList =await  _repository.GetAll(u => u.User)
                        .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.Product)
                        .ToListAsync();

            // Execute the query and get the result with product 
            //var resultList = await queryableResult.Include(c => c.CartItems).ThenInclude(p => p.Product).ToListAsync();

            if (resultList != null && resultList.Count > 0)
            {


               var dtoList = _mapper.Map<List<ReadCartDto>>(resultList);
                return CreateResponse(true, dtoList, "Carts retrieved successfully.", 204);
            }
            if (resultList != null && resultList.Count == 0)
            {
               
                return CreateResponse(true, null, "Carts retrieved successfully,but no cart exest", 200);
            }

            return CreateResponse(false, null, "Carts not found.", 404);
        }

        public async override Task<GeneralRespons> GetByIdAsync(int id)
        {
            var idExsist= _repository.GetAll().Any(c=>c.Id==id);
            if(idExsist)
            {
             var cart = await _repository.GetAll(c=>c.Id==id, u => u.User)
                                                 .Include(c => c.CartItems)
                                                 .ThenInclude(ci => ci.Product)
                                                 .FirstOrDefaultAsync();

                return CreateResponse(true, _mapper.Map<ReadCartDto>(cart), $"{typeof(Cart).Name} retrieved successfully.", 200);

            }else return CreateResponse(false, null, $"no cart with this id.", 404);
            //var queryableResult =  _repository.GetAll(u => u.User, c => c.CartItems, c => c.CartItems.Select(p => p.Product));
            //var result =await queryableResult.FirstOrDefaultAsync();

            //if (result != null)
            //{
            //    var dto = _mapper.Map<ReadCartDto>(result);
            //    
            //}

            //

        }


        public async Task<GeneralRespons> GetByUserIdAsync(string userId)
        {
            var userExists = await _userManager.FindByIdAsync(userId);
            if (userExists == null)
            {
                return CreateResponse(false, null, "No user with this id.", 404); // Not Found
            }

            try
            {

                //return await base.GetAllByConditionAndIncludes(
                //        x => x.UserId == userId,
                //        c => c.CartItems, // Include CartItems
                //        c => c.CartItems.Select(x => x.Product)
                //     );

                var cart = await _repository.GetByConditionAsync(x => x.UserId == userId)
                    .Include(c => c.CartItems)
                    .ThenInclude(p => p.Product)
                    .FirstOrDefaultAsync();

                //   var cart = await _repository.GetByUserIdAsync(userId);
                if (cart == null)
                {
                    return CreateResponse(false, null, "No cart found for the given name.", 404); // Not Found
                }
                var readDto = _mapper.Map<ReadCartDto>(cart);
                return CreateResponse(true, readDto, "cart retrieved successfully", 200); // OK
            }
            catch (Exception ex)
            {
                return CreateResponse(false, null, $"An error occurred while processing your request: {ex.Message}. Please try again later.", 500, new List<string> { ex.Message }); // Internal Server Error
            }
        }
        public async Task RemoveCartItems(IEnumerable<CartItem> cartItems)
        {
            await _repository.RemoveCartItemsAsync(cartItems);
            //save one time after all changes
            await _repository.SaveChangesAsync();
        }

        //public async Task<GeneralRespons> UpdateCartItemsInCart(int id, List<UpdateCartItemsInCartDto> cartItemsInCart) 
        //{
        //    var idExsist =  _repository.GetAll().Any(c => c.Id == id);
        //    if (!idExsist)
        //    {
        //        return CreateResponse(false, null, $"no cart with this id.", 404);
        //    }

        //    var cartitems = _mapper.Map<List<CartItem>>(cartItemsInCart);

        //    foreach (var cartItem in cartitems)
        //    {
        //        await _cartItemRepo.UpdateAsync(cartItem);
        //    }

        //    var cart = await _repository.GetAll(c => c.Id == id, u => u.User)
        //                                            .Include(c => c.CartItems)
        //                                            .ThenInclude(ci => ci.Product)
        //                                            .FirstOrDefaultAsync();

        //    return CreateResponse(true, _mapper.Map<ReadCartDto>(cart), $"{typeof(Cart).Name} retrieved successfully.", 200);
        //}
    }
}
