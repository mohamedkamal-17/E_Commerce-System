using AutoMapper;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.GeneralManager
{
    public class Manager<T, TAddDto, TUpdateDto> : IManager<T, TAddDto, TUpdateDto> where T : class
        where TAddDto : class
        where TUpdateDto : class
    {
        private readonly IRepository<T> _repository;
        private readonly IMapper _mapper;

        public Manager(IRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private GeneralRespons CreateResponse(bool success, object? model, string message, List<string>? errors = null)
        {
            return new GeneralRespons
            {
                Success = success,
                Model = model,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }

        public async Task<GeneralRespons> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();
            if (result != null && result.Count > 0)
            {
                return CreateResponse(true, result, $"{nameof(T)}s retrieved successfully");
            }
            return CreateResponse(false, null, $"{nameof(T)} not found");
        }

        public async Task<GeneralRespons> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result != null)
            {
                return CreateResponse(true, result, $"{nameof(T)} retrieved successfully");
            }
            return CreateResponse(false, null, $"{nameof(T)} not found");
        }

        // Method for adding an entity using TAddDto
        public async Task<GeneralRespons> AddAsync(TAddDto addDto)
        {
            var response = new GeneralRespons();
            T entity = _mapper.Map<T>(addDto);  // Map the AddDto to the entity
            try
            {
                await _repository.AddAsync(entity);
                response.Success = true;
                response.Message = $"{nameof(T)} added successfully";
                response.Model = entity;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error adding {nameof(T)}: {ex.Message}";
                response.Errors = new List<string> { ex.Message };
                return response;
            }
        }

        // Method for updating an entity using TUpdateDto
        public async Task<GeneralRespons> UpdateAsync(TUpdateDto updateDto)
        {
            var response = new GeneralRespons();
            T entity = _mapper.Map<T>(updateDto);  // Map the UpdateDto to the entity
            try
            {
                await _repository.UpdateAsync(entity);
                response.Success = true;
                response.Message = $"{nameof(T)} updated successfully";
                response.Model = entity;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error updating {nameof(T)}: {ex.Message}";
                response.Errors = new List<string> { ex.Message };
                return response;
            }
        }

        public async Task<GeneralRespons> DeleteAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                {
                    return CreateResponse(false, null, $"{nameof(T)} not found for deletion");
                }

                await _repository.DeleteAsync(entity);
                return CreateResponse(true, entity, $"{nameof(T)} deleted successfully");
            }
            catch (Exception ex)
            {
                return CreateResponse(false, null, $"Error deleting {nameof(T)}: {ex.Message}", new List<string> { ex.Message });
            }
        }
    }
}
