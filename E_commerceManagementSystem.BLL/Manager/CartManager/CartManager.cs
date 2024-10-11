using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.CartDto;
using E_commerceManagementSystem.BLL.Dto.CategoryDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.CartRepository;
using E_commerceManagementSystem.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public override async Task<GeneralRespons> GetAllAsync()
        {
            // Include the navigation properties you need
            var queryableResult = await _repository.GetAllWithIncludesAsync(u=>u.User);

            // Execute the query and get the result with product 
            var resultList = await queryableResult.Include(c => c.CartItems).ThenInclude(p => p.Product).ToListAsync();
            
            if (resultList != null && resultList.Count > 0)
            {
                var dtoList = _mapper.Map<List<ReadCartDto>>(resultList);
                return CreateResponse(true, dtoList, "Carts retrieved successfully.", 200);
            }

            return CreateResponse(false, null, "Carts not found.", 404);
        }

        public async override Task<GeneralRespons> GetByIdAsync(int id)
        {
            var queryableResult = await _repository.GetAllWithIncludesAsync(u => u.User);
            var result = await queryableResult
                .Include(c=>c.CartItems)
                .ThenInclude(p=>p.Product)
                .SingleOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);

            if (result != null)
            {
                var dto = _mapper.Map<ReadCartDto>(result);
                return CreateResponse(true, dto, $"{typeof(Cart).Name} retrieved successfully.", 200);
            }

            return CreateResponse(false, null, $"no cart with this id.", 404);

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
                var cart = await _repository.GetByConditionAsync(x => x.UserId == userId)
                    .Include(c=>c.CartItems)
                    .ThenInclude(p=>p.Product)
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
    }
}
