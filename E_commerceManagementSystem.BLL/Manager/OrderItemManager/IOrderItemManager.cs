using E_commerceManagementSystem.BLL.Dto.OrederItemDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.OrderItemManager
{
    public interface IOrderItemManager : IManager<OrderItem, ReadOrderItemDto, AddOrderItemDto,  UpdateOrderItemDto>
    {
        Task<GeneralRespons> GetByOrderIdAsync(int orderId);
    }
}
