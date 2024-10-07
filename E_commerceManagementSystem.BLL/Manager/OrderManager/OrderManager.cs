using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.OrderDto;
using E_commerceManagementSystem.BLL.Dto.ProductDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.BLL.Manager.ProductManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.OrederRepository;
using E_commerceManagementSystem.DAL.Reposatories.ProductRepository;
using E_commerceManagementSystem.DAL.Reposatories.ReviewRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            _repository =  repository;
            _userManager = userManager;
            _mapper =   mapper;
        }

        public async Task<GeneralRespons> GetByUserIdAsync(string userId)
        {
            var userExists = await _userManager.FindByIdAsync(userId);
            if (userExists == null)
            {
                return new GeneralRespons
                {
                    Success = false,
                    Message = "No user with this id"
                };
            }
            var result = await _repository.GetByUserIdAsync(userId);
            if(result == null)
            {
                return new GeneralRespons
                {
                    Success = false,
                    Message = "this user has not any order"
                };

            }
            var listOfResult = await result.ToListAsync();
            var orderReadDto = _mapper.Map<ICollection<ReadOrderDto>>(listOfResult);
            return new GeneralRespons
            {
                Success = true,
                Model = orderReadDto
            };
        }
    }
}
