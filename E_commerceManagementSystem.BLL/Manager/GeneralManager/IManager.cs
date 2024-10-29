using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using System.Linq.Expressions;

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
        Task<GeneralRespons> GetAll();
        Task<GeneralRespons> GetAll(Expression<Func<T, bool>> condition);
        Task<GeneralRespons> GetAll(params Expression<Func<T, object>>[] includes);
        Task<GeneralRespons> GetAllByConditionAndIncludes(Expression<Func<T, bool>> condition,
            params Expression<Func<T, object>>[] includes);
        Task<GeneralRespons> GetByIdAsync(int id);
        Task<GeneralRespons> AddAsync(TAddDto dto);
        Task<GeneralRespons> UpdateAsync(int id, TUpdateDto dto);
        Task<GeneralRespons> DeleteAsync(int id);

    }

}

