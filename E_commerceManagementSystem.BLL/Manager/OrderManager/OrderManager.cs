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
        public override async Task<GeneralRespons> GetAllAsync()
        {
            return await base.GetAll(o => o.OrderItems, o => o.User);
        }
        public async Task<GeneralRespons> GetByUserIdAsync(string userId)
        {

            return await base.GetAllByConditionAndIncludes(o => o.UserId == userId, o => o.OrderItems, o => o.User);
                                               

             
        }

      
    }
}
