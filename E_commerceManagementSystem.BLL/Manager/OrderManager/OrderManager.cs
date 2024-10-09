using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_commerceManagementSystem.BLL.Dto.OrderDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.OrederRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.OrderManager
{
    public class OrderManager : Manager<Order, ReadOrderDto, AddOrderDto, UpdateOrderDto>, IOrderManager
    {
        private readonly IOrderRepo _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public OrderManager(IOrderRepo repository, IMapper mapper, UserManager<ApplicationUser> userManager)
            : base(repository, mapper)
        {
            _repository = repository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<GeneralRespons> GetByUserIdAsync(string userId)
        {
            try
            {
                var userExists = await _userManager.FindByIdAsync(userId);
                if (userExists == null)
                {
                    return CreateResponse(false, null, "No user with this ID", 404); // Not Found
                }

                var result = await _repository.GetByConditionAsync(or => or.UserId == userId)
                                               .ProjectTo<ReadOrderDto>(_mapper.ConfigurationProvider)
                                               .ToListAsync();

                if (result == null || !result.Any())
                {
                    return CreateResponse(false, null, "This user has not placed any orders.", 204); // No Content
                }

                return CreateResponse(true, result, "Orders retrieved successfully.", 200); // OK
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error retrieving orders: {ex.Message}";
                var errors = new List<string> { ex.Message };
                return CreateResponse(false, null, errorMessage, 500, errors); // Internal Server Error
            }
        }

      
    }
}
