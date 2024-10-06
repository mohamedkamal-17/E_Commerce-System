using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.GeneralManager
{
    public interface IManager<T, TAddDto, TUpdateDto> 
        where T : class
        where TAddDto : class
        where TUpdateDto : class
    {
        Task<GeneralRespons> GetAllAsync();
        Task<GeneralRespons> GetByIdAsync(int id);
        Task<GeneralRespons> AddAsync(TAddDto dto);
        Task<GeneralRespons> UpdateAsync(TUpdateDto dto);
        Task<GeneralRespons> DeleteAsync(int id);
    }

}

