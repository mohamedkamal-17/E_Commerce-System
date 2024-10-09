using E_commerceManagementSystem.BLL.Dto.InventoryDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.InventoryManager
{
    public interface IInventoryManager:IManager<Inventory,ReadInventoryDto,AddInventoryDto,UpdateInventoryDto>
    {
        Task<GeneralRespons> GetByProductId(int productId);
    }
}
