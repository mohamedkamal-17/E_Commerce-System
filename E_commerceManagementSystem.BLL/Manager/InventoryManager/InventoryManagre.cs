using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.InventoryDto;
using E_commerceManagementSystem.BLL.Dto.ProductDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.InventoryRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.InventoryManager
{
    public class InventoryManagre:Manager<Inventory, ReadInventoryDto, AddInventoryDto, UpdateInventoryDto>,IInventoryManagre
    {
        private readonly IInventoryRepo _repository;
        private readonly IMapper _mapper;

        public InventoryManagre(IInventoryRepo repository,IMapper mapper):base(repository, mapper)
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


       
        public async Task<GeneralRespons> GetByProductId(int productId)
        {
            try
            {
                var inventories = await _repository.GetAllAsync();
                var resultLoist =  inventories.FirstOrDefault(inv=>inv.ProductId == productId);

                if (resultLoist != null )
                {
                    var readDtos = _mapper.Map<ReadInventoryDto>(resultLoist);

                    return CreateResponse(true, readDtos, "inventory retrieved successfully by price.");
                }
                return CreateResponse(false, null, "No inventory found for the given price.");
            }
            catch (Exception ex)
            {
                return CreateResponse(false, null, $"Error retrieving products: {ex.Message}", new List<string> { ex.Message });
            }
        }

      
}
