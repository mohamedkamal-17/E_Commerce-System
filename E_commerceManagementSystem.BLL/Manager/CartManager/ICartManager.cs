using E_commerceManagementSystem.BLL.Dto.CartDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.CartManager
{
    public interface ICartManager : IManager<Cart,ReadCartDto,AddCartDto,UpdateCartDto>
    {
        Task<GeneralRespons> GetByUserIdAsync(string userId);
    }
}
