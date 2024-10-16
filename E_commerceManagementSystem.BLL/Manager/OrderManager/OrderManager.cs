using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_commerceManagementSystem.BLL.Dto.CartDto;
using E_commerceManagementSystem.BLL.Dto.OrderDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.CartManager;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.CartRepository;
using E_commerceManagementSystem.DAL.Reposatories.OrederRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
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
        private readonly ICartRepo _cartRepo;

        public OrderManager(IOrderRepo repository, IMapper mapper, UserManager<ApplicationUser> userManager, ICartRepo cartRepo)
            : base(repository, mapper)
        {
            _repository = repository;
            _userManager = userManager;
            _mapper = mapper;
            _cartRepo = cartRepo;
        }
        public override async Task<GeneralRespons> GetAllAsync()
        {
            return await base.GetAll(o => o.OrderItems, o => o.User);
        }
        public override Task<GeneralRespons> GetByIdAsync(int id)
        {
            return base.GetAllByConditionAndIncludes(o => o.Id == id, o => o.OrderItems, o => o.User);
        }
        public async Task<GeneralRespons> GetByUserIdAsync(string userId)
        {

            return await base.GetAllByConditionAndIncludes(o => o.UserId == userId, o => o.OrderItems, o => o.User);
 
        }

        public override async Task<GeneralRespons> AddAsync(AddOrderDto addDto)
        {
            var cart = await _cartRepo.GetAll(c => c.Id == addDto.CartId, u => u.User)
                                                 .Include(c => c.CartItems)
                                                 .ThenInclude(ci => ci.Product)
                                                 .FirstOrDefaultAsync();


            if (cart == null || cart.CartItems == null || !cart.CartItems.Any())
            {
                return CreateResponse(false, null, "Cart is empty or does not exist", 400);
                
            }


            double totalPrice = cart.CartItems.Sum(item => item.Product.Price * item.Quantity);

            var order = new Order
            {
                UserId = cart.UserId,
                Address = addDto.Address,
                TotalPrice = totalPrice,
                PaymentId = addDto.PaymentId,
                OrderItems = _mapper.Map<List<OrderItem>>(cart.CartItems) 
            };

            try
            {
                await _repository.AddAsync(order);

                var readOrderDto = _mapper.Map<ReadOrderDto>(order);

              await _cartRepo.RemoveCartItemsAsync(cart.CartItems);
                return CreateResponse(true, readOrderDto, "Order added successfully", 201);
            }
            catch (Exception ex)
            {
                return CreateResponse(false, null, $"Error adding order: {ex.Message}", 500, new List<string> { ex.InnerException?.ToString() });
            }
        }
    }
}