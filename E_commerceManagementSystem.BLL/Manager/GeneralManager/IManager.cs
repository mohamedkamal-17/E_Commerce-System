using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.GeneralManager
{
    public interface IManager<T> where T : class
    {
        Task<GeneralRespons> GetAllAsync();
        Task<GeneralRespons> GetByIdAsync(int id);
        Task<GeneralRespons> AddAsync(object dto);
        Task<GeneralRespons> UpdateAsync(object dto);
        Task<GeneralRespons> DeleteAsync(int id);
    }

}

