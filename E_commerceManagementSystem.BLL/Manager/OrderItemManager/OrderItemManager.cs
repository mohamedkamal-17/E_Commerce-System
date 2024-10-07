using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.OrederItemDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.OrederItemRepository;
using E_commerceManagementSystem.DAL.Reposatories.OrederRepository;
using E_commerceManagementSystem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.OrderItemManager
{
    public class OrderItemManager : Manager<OrderItem, ReadOrderItemDto, AddOrderItemDto, UpdateOrderItemDto>, IOrderItemManager
    {
        private readonly IOrderItemRepo _orderItemRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly IMapper _mapper;

        public OrderItemManager(IOrderItemRepo repository, IMapper mapper, IOrderRepo orderRepo) : base(repository, mapper)
        {
            _orderItemRepo = repository;
            _orderRepo = orderRepo;
            _mapper =   mapper;
        }

        public async Task<GeneralRespons> GetByOrderIdAsync(int orderId)
        {
            var orderExists = await _orderRepo.GetByIdAsync(orderId);
            if (orderExists == null)
            {
                return new GeneralRespons
                {
                    Success = false,
                    Message = "No order with this Id"
                };
            }
            var QueryorderItems = await _orderItemRepo.GetByOrderIdAsync(orderId);
            if (QueryorderItems == null)
            {
                return new GeneralRespons
                {
                    Success = false,
                    Message = "No Items in this Order"
                };
            }
            var orderItems = await QueryorderItems.ToListAsync();
            var ResultDto = _mapper.Map<ICollection<ReadOrderItemDto>>(orderItems);
            return new GeneralRespons
            {
                Success = true,
                Model = ResultDto
            };
        }
    }
}