using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.ShippingDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.BLL.Manager.ShippingManger;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.ShippingRepository;

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

        public override async Task<GeneralRespons> GetAllAsync()
        {
            return await base.GetAll(order => order.User);


        }
        public override async Task<GeneralRespons> GetByIdAsync(int id)
        {
            return await base.GetAllByConditionAndIncludes(order => order.Id == id, ve => ve.User);



        }
        public async Task<GeneralRespons> GetByOrderIdAsync(int id)
        {
            return await base.GetAllByConditionAndIncludes(order => order.OrderId == id, order => order.User);

        }

        public async Task<GeneralRespons> GetByShippingStateAsync(string shippingStatus)
        {
            return await base.GetAllByConditionAndIncludes(order => order.ShippingStatus == shippingStatus, order => order.User);

        }

        public async Task<GeneralRespons> GetByShippingDateAsync(DateTime shippingDate)
        {
            return await base.GetAllByConditionAndIncludes(order => order.ShippedDate == shippingDate, order => order.User);

        }

        public async Task<GeneralRespons> GetByTrackingNumberAsync(string trackingNumber)
        {
            return await base.GetAllByConditionAndIncludes(order => order.TrackingNumber == trackingNumber, order => order.User);


        }


    }


}

