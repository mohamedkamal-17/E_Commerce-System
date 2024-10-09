using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.ShippingDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.ShippingRepository;
using Microsoft.EntityFrameworkCore;
using System.Net;
using E_commerceManagementSystem.BLL.Manager.ShippingManger;
using System.Linq.Expressions;
using AutoMapper.QueryableExtensions; // Added for HTTP status codes

namespace E_commerceManagementSystem.BLL.Manager.ShippingManager
{
    public class ShippingManager : Manager<Shipping, ReadShippingDto, AddShippingDto, UpdateShippingDto>, IShippingManager
    {
        private readonly IShippingRepo _repository;
        private readonly IMapper _mapper;

        public ShippingManager(IShippingRepo repository, IMapper mapper)
            : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private GeneralRespons CreateResponse(bool success, object? model, string message, int statusCode, List<string>? errors = null)
        {
            return new GeneralRespons
            {
                Success = success,
                Model = model,
                Message = message,
                StatusCode = statusCode, // Add StatusCode here
                Errors = errors ?? new List<string>()
            };
        }

        public async Task<GeneralRespons> GetByOrderIdAsync(int id)
        {
            return await GetByConditionAsync(
                order => order.Id == id,
                $"{typeof(Shipping).Name} retrieved successfully",
                $"{typeof(Shipping).Name} not retrieved successfully",
                "Not found Order With This Id",
                (int)HttpStatusCode.OK, // Success
                (int)HttpStatusCode.NotFound // Not Found
            );
        }

        public async Task<GeneralRespons> GetByShippingStateAsync(string shippingStatus)
        {
            return await GetByConditionAsync(
                order => order.ShippingStatus == shippingStatus,
                $"{typeof(Shipping).Name} retrieved successfully",
                $"{typeof(Shipping).Name} not retrieved successfully",
                "Not found Order With This Shipping Status",
                (int)HttpStatusCode.OK, // Success
                (int)HttpStatusCode.NotFound // Not Found
            );
        }

        public async Task<GeneralRespons> GetByShippingDateAsync(DateTime shippingDate)
        {
            return await GetByConditionAsync(
                order => order.ShippedDate == shippingDate,
                $"{typeof(Shipping).Name} retrieved successfully",
                $"{typeof(Shipping).Name} not retrieved successfully",
                "Not found Order With This Shipping Date",
                (int)HttpStatusCode.OK, // Success
                (int)HttpStatusCode.NotFound // Not Found
            );
        }

        public async Task<GeneralRespons> GetByTrackingNumberAsync(int trackingNumber)
        {
            return await GetByConditionAsync(
                order => order.TrackingNumber == trackingNumber,
                $"{typeof(Shipping).Name} retrieved successfully",
                $"{typeof(Shipping).Name} not retrieved successfully",
                "Not found Order With This Tracking Number",
                (int)HttpStatusCode.OK, // Success
                (int)HttpStatusCode.NotFound // Not Found
            );
        }

        private async Task<GeneralRespons> GetByConditionAsync(
            Expression< Func<Shipping, bool>> predicate,
            string successMessage,
            string failureMessage,
            string notFoundError,
            int successStatusCode,
            int notFoundStatusCode
        )
        {
            try
            {
                var shippingDtos = await _repository.GetByConditionAsync(predicate)
                    .ProjectTo<ReadShippingDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                // Use ProjectTo to map from the queryable to ReadShippingDto
                if (shippingDtos.Any())
                {
                    // Return success response if any shipping DTOs were found
                    return CreateResponse(true, shippingDtos, successMessage, successStatusCode);
                }

                return CreateResponse(false, null, failureMessage, notFoundStatusCode, new List<string> { notFoundError });
            }
            catch (Exception ex)
            {
                return CreateResponse(false, null, $"Server-side Error: {ex.Message}", (int)HttpStatusCode.InternalServerError, new List<string> { ex.Message });
            }
        }
    }
}
