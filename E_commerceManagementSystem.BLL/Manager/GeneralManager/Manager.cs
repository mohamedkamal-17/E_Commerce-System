﻿using AutoMapper;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.GeneralManager
{
    public class Manager<T, TAddDto, TUpdateDto> : IManager<T, TAddDto, TUpdateDto>
        where T : class
        where TAddDto : class
        where TUpdateDto : class
    {
        private readonly IRepository<T> _repository;
        private readonly IMapper _mapper;

        public Manager(IRepository<T> repository, IMapper mapper)
        {
            _repository = repository;// ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;//?? throw new ArgumentNullException(nameof(mapper));
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
                return CreateResponse(true, result, $"{nameof(T)}s retrieved successfully.");
            }
            return CreateResponse(false, null, $"{nameof(T)}s not found.");
        }

        public async Task<GeneralRespons> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result != null)
            {
                return CreateResponse(true, result, $"{nameof(T)} retrieved successfully.");
            }
            return CreateResponse(false, null, $"{nameof(T)} not found.");
        }

        // Method for adding an entity using TAddDto
        public async Task<GeneralRespons> AddAsync(TAddDto addDto)
        {
            if (addDto == null)
            {
                return CreateResponse(false, null, "Add DTO cannot be null.");
            }

            T entity = _mapper.Map<T>(addDto); // Map the AddDto to the entity

            try
            {
                await _repository.AddAsync(entity);
                return CreateResponse(true, entity, $"{nameof(T)} added successfully.");
            }
            catch (Exception ex)
            {
                return CreateResponse(false, null, $"Error adding {nameof(T)}: {ex.Message}", new List<string> { ex.Message });
            }
        }

        // Method for updating an entity using TUpdateDto
        public async Task<GeneralRespons> UpdateAsync(int id, TUpdateDto updateDto)
        {
            if (updateDto == null)
            {
                return CreateResponse(false, null, "Update DTO cannot be null.");
            }

            T entity = _mapper.Map<T>(updateDto); // Map the UpdateDto to the entity

            try
            {
                // Optionally, check if the entity exists before updating
                var existingEntity = await _repository.GetByIdAsync(id);
                if (existingEntity == null)
                {
                    return CreateResponse(false, null, $"{nameof(T)} with ID {id} not found.");
                }

                await _repository.UpdateAsync(entity);
                return CreateResponse(true, entity, $"{nameof(T)} updated successfully.");
            }
            catch (Exception ex)
            {
                return CreateResponse(false, null, $"Error updating {nameof(T)}: {ex.Message}", new List<string> { ex.Message });
            }
        }

        public async Task<GeneralRespons> DeleteAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                {
                    return CreateResponse(false, null, $"{nameof(T)} with ID {id} not found for deletion.");
                }

                await _repository.DeleteAsync(entity);
                return CreateResponse(true, entity, $"{nameof(T)} deleted successfully.");
            }
            catch (Exception ex)
            {
                return CreateResponse(false, null, $"Error deleting {nameof(T)}: {ex.Message}", new List<string> { ex.Message });
            }
        }
    }
}