using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.OrederItemDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.OrederItemRepository;
using E_commerceManagementSystem.DAL.Reposatories.OrederRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.OrderItemManager
{
    public class OrderItemManager : Manager<OrderItem, ReadOrderItemDto, AddOrderItemDto, UpdateOrderItemDto>, IOrderItemManager
    {
        private readonly IOrderItemRepo _orderItemRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly IMapper _mapper;

        public OrderItemManager(IOrderItemRepo repository, IMapper mapper, IOrderRepo orderRepo)
            : base(repository, mapper)
        {
            _orderItemRepo = repository;
            _orderRepo = orderRepo;
            _mapper = mapper;
        }

        public async Task<GeneralRespons> GetByOrderIdAsync(int orderId)
        {
            try
            {
                // Check if the order exists using GetByConditionAsync
                var order = await _orderRepo.GetByIdAsync(orderId);
                if (order == null)
                {
                    return CreateResponse(false, null, "No order with this ID", 404); // Not Found
                }

                var orderItems = await _orderItemRepo.GetByConditionAsync(oi => oi.OrderId == orderId).ToListAsync();
                if (!orderItems.Any()) // Check if there are any items
                {
                    return CreateResponse(false, null, "No items in this order", 204); // No Content
                }

                var resultDto = _mapper.Map<List<ReadOrderItemDto>>(orderItems);
                return CreateResponse(true, resultDto, "Order items retrieved successfully.", 200); // OK
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error retrieving order items: {ex.Message}";
                var errors = new List<string> { ex.Message };
                return CreateResponse(false, null, errorMessage, 500, errors); // Internal Server Error
            }
        }

       
    }
}
