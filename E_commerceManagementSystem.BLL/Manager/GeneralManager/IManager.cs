using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.GeneralManager
{
    public interface IManager<T, TReadDto, TAddDto, TUpdateDto> 
        where T : class
        where TReadDto : class
        where TAddDto : class
        where TUpdateDto : class

    {
        Task<GeneralRespons> GetAllAsync();
       // Task<GeneralRespons> GetAllWithIncludesAsync(params Expression<Func<T, object>>[] includes);

        Task<GeneralRespons> GetByIdAsync(int id);
        Task<GeneralRespons> AddAsync(TAddDto dto);
        Task<GeneralRespons> UpdateAsync(int id ,TUpdateDto dto);
        Task<GeneralRespons> DeleteAsync(int id);
    }

}

