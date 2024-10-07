using E_commerceManagementSystem.BLL.Dto.OrderDto;
using E_commerceManagementSystem.BLL.Dto.ProductDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.OrderManager
{
    public interface IOrderManager : IManager<Order, ReadOrderDto, AddOrderDto, UpdateOrderDto>
    {
        Task<GeneralRespons> GetByUserIdAsync(string userId);
    }
}
