using AutoMapper;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.CartItemManager;
using E_commerceManagementSystem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net; // Added for HTTP status codes
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.GeneralManager
{
    public class Manager<T, TReadDto, TAddDto, TUpdateDto> : IManager<T, TReadDto, TAddDto, TUpdateDto>
        where T : class
        where TReadDto : class
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
        
        public GeneralRespons CreateResponse(bool success, object? model, string message, int statusCode, List<string>? errors = null)
        {
            return new GeneralRespons
            {
                Success = success,
                Model = model,
                Message = message,
                StatusCode = statusCode, // Set StatusCode here
                Errors = errors ?? new List<string>()
            };
        }

        public async Task<GeneralRespons> GetAllAsync()
        {
            var queryableResult = await _repository.GetAllAsync();
            var resultList = await queryableResult.ToListAsync();

            if (resultList != null && resultList.Count > 0)
            {
                var dtoList = _mapper.Map<List<TReadDto>>(resultList);
                return CreateResponse(true, dtoList, $"{typeof(T).Name}s retrieved successfully.", 200);
            }

            return CreateResponse(false, null, $"{typeof(T).Name}s not found.", 404);
        }

        public async Task<GeneralRespons> GetAllWithIncludesAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = await _repository.GetAllAsync();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var resultList = await query.ToListAsync();

            if (resultList != null && resultList.Count > 0)
            {
                var dtoList = _mapper.Map<List<TReadDto>>(resultList);
                return CreateResponse(true, dtoList, $"{typeof(T).Name}s retrieved successfully.", 200);
            }

            return CreateResponse(false, null, $"{typeof(T).Name}s not found.", 404);
        }

        public async Task<GeneralRespons> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result != null)
            {
                var dto = _mapper.Map<TReadDto>(result);
                return CreateResponse(true, dto, $"{typeof(T).Name} retrieved successfully.",200);
            }

            return CreateResponse(false, null, $"{typeof(T).Name} not found.",404);
        }

        public virtual async Task<GeneralRespons> AddAsync(TAddDto addDto)
        {
            if (addDto == null)
            {
                return CreateResponse(false, null, "Add DTO cannot be null.",400);
            }

            T entity = _mapper.Map<T>(addDto); // Map the AddDto to the entity

            try
            {
                await _repository.AddAsync(entity);
                return CreateResponse(true, entity, $"{typeof(T).Name} added successfully.", 201);
            }
            catch (Exception ex)
            {
                return CreateResponse(false, null, $"Error adding {typeof(T).Name}: {ex.Message}",500, new List<string> { ex.Message });
            }
        }

        public async Task<GeneralRespons> UpdateAsync(int id, TUpdateDto updateDto)
        {
            if (updateDto == null)
            {
                return CreateResponse(false, null, "Update DTO cannot be null.", 400);
            }

            T entity = _mapper.Map<T>(updateDto); // Map the UpdateDto to the entity

            try
            {
                var existingEntity = await _repository.GetByIdAsync(id);
                if (existingEntity == null)
                {
                    return CreateResponse(false, null, $"{typeof(T).Name} with ID {id} not found.", 404);
                }

                await _repository.UpdateAsync(entity);
                return CreateResponse(true, entity, $"{typeof(T).Name} updated successfully.",200);
            }
            catch (Exception ex)
            {
                return CreateResponse(false, null, $"Error updating {typeof(T).Name}: {ex.Message}", 500, new List<string> { ex.Message });
            }
        }

        public async Task<GeneralRespons> DeleteAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                {
                    return CreateResponse(false, null, $"{typeof(T).Name} with ID {id} not found for deletion.",404);
                }

                await _repository.DeleteAsync(entity);
                return CreateResponse(true, null, $"{typeof(T).Name} deleted successfully.", 200);
            }
            catch (Exception ex)
            {
                return CreateResponse(false, null, $"Error deleting {typeof(T).Name}: {ex.Message}",500, new List<string> { ex.Message });
            }
        }
    }
}
