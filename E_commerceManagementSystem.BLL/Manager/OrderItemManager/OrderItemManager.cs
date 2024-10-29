using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.OrederItemDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.OrederItemRepository;
using E_commerceManagementSystem.DAL.Reposatories.OrederRepository;

namespace E_commerceManagementSystem.BLL.Manager.OrderItemManager
{
    public class OrderItemManager : Manager<OrderItem, ReadOrderItemDto, AddOrderItemDto, UpdateOrderItemDto>, IOrderItemManager
    {
        private readonly IOrderItemRepo _orderItemRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly IMapper _mapper;

        public OrderItemManager(IOrderItemRepo repository, IMapper mapper, IOrderRepo orderRepo)
            : base(repository, mapper)
        {
            _orderItemRepo = repository;
            _orderRepo = orderRepo;
            _mapper = mapper;
        }
        public override async Task<GeneralRespons> GetAllAsync()
        {
            return await base.GetAll(orit => orit.Product);
        }
        public override async Task<GeneralRespons> GetByIdAsync(int id)
        {
            return await base.GetAllByConditionAndIncludes(orit => orit.Id == id, orit => orit.Product);
        }

        public async Task<GeneralRespons> GetByOrderIdAsync(int orderId)
        {

            // Check if the order exists using GetByConditionAsync
            var order = await _orderRepo.GetByIdAsync(orderId);
            if (order == null)
            {
                return CreateResponse(false, null, "No order with this ID", 404); // Not Found
            }

            return await base.GetAll(oi => oi.OrderId == orderId);


        }


    }
}
