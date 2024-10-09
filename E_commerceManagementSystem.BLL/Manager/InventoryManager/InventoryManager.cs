using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.InventoryDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.InventoryRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.InventoryManager
{
    public class InventoryManager : Manager<Inventory, ReadInventoryDto, AddInventoryDto, UpdateInventoryDto>, IInventoryManager
    {
        private readonly IInventoryRepo _repository;
        private readonly IMapper _mapper;

        public InventoryManager(IInventoryRepo repository, IMapper mapper)
            : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

    

        public async Task<GeneralRespons> GetByProductId(int productId)
        {
            try
            {
                var inventory = await _repository.GetByConditionAsync(inv => inv.ProductId == productId).FirstOrDefaultAsync();

                if (inventory != null)
                {
                    var readDto = _mapper.Map<ReadInventoryDto>(inventory);
                    return CreateResponse(true, readDto, "Inventory retrieved successfully.", 200); // OK
                }

                return CreateResponse(false, null, "No inventory found for the given product ID.", 404); // Not Found
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error retrieving inventory: {ex.Message}";
                var errors = new List<string> { ex.Message };
                return CreateResponse(false, null, errorMessage, 500, errors); // Internal Server Error
            }
        }
    }
}
