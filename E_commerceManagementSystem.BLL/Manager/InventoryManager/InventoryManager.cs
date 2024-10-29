using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.InventoryDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.InventoryRepository;

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
            return await base.GetAll(inv => inv.ProductId == productId);
        }
    }
}
