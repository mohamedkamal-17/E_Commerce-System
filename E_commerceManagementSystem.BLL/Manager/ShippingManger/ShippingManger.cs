using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.ShippingDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.ProductRepository;
using E_commerceManagementSystem.DAL.Reposatories.ShippingRepository;
using Microsoft.EntityFrameworkCore;

namespace E_commerceManagementSystem.BLL.Manager.ShippingManger
{
    public class ShippingManger:Manager<Shipping, ReadShippingDto,AddShippingDto, UpdateShippingDto>,IShippingManger
    {
        private readonly IShippingRepo _rpository;
        private readonly IMapper _mapper;
        
        public ShippingManger(IShippingRepo rpository, IMapper mapper)
       
   
        : base(rpository, mapper)
        {
            _rpository = rpository;
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
        public async Task<GeneralRespons> GetByOrderIdAsync(int id)
        {

            try
            {
                var qurabelList = await _rpository.GetAllAsync();
                var order = qurabelList.FirstOrDefault(or => or.Id == id);

                if (order != null)
                {
                    ReadShippingDto shipingdto = _mapper.Map<ReadShippingDto>(order);
                    return CreateResponse(true,
                        shipingdto, $"{typeof(Shipping).Name} retrieved successfully", null);
                }
                return CreateResponse(false, null, $"{typeof(Shipping).Name} not  retrieved successfully", new List<string> { "Not found Order With This Id" });
            }
            catch (Exception ex) { 
            
            return CreateResponse(false,null, ex.Message, new List<string>{ "Server sid Error"});
            
            }
        }

        public async Task<GeneralRespons> GetByShippingState(string shipingStatu)
        {
            try
            {
                var qurabelList = await _rpository.GetAllAsync();
                var orders = await qurabelList.Where(or => or.ShippingStatus == shipingStatu).ToListAsync();

                if (orders != null)
                {
                    var shipingdto = _mapper.Map<List<ReadShippingDto>>(orders);
                    return CreateResponse(true,
                        shipingdto, $"{typeof(Shipping).Name} retrieved successfully", null);
                }
                return CreateResponse(false, null, $"{typeof(Shipping).Name} not  retrieved successfully", new List<string> { "Not found Order With This shipingStatu" });
            }
            catch (Exception ex)
            {

                return CreateResponse(false, null, ex.Message, new List<string> { "Server sid Error" });

            }
        }

            public async  Task<GeneralRespons> GetByShippingDate(DateTime shippingDate)
        {
            try
            {
                var qurabelList = await _rpository.GetAllAsync();
                var orders = await qurabelList.Where(or => or.ShippedDate == shippingDate).ToListAsync();

                if (orders != null)
                {
                    var shipingdto = _mapper.Map<List<ReadShippingDto>>(orders);
                    return CreateResponse(true,
                        shipingdto, $"{typeof(Shipping).Name} retrieved successfully", null);
                }
                return CreateResponse(false, null, $"{typeof(Shipping).Name} not  retrieved successfully", new List<string> { "Not found Order With This shippingDate" });
            }
            catch (Exception ex)
            {

                return CreateResponse(false, null, ex.Message, new List<string> { "Server sid Error" });

            }

        }

        public async Task<GeneralRespons> GetByTrackingNumber(int trackingNumber)
        {
            try
            {
                var qurabelList = await _rpository.GetAllAsync();
                var orders = await qurabelList.Where(or => or.TrackingNumber == trackingNumber).ToListAsync();

                if (orders != null)
                {
                    var shipingdto = _mapper.Map<List<ReadShippingDto>>(orders);
                    return CreateResponse(true,
                        shipingdto, $"{typeof(Shipping).Name} retrieved successfully", null);
                }
                return CreateResponse(false, null, $"{typeof(Shipping).Name} not  retrieved successfully", new List<string> { "Not found Order With This trackingNumber" });
            }
            catch (Exception ex)
            {

                return CreateResponse(false, null, ex.Message, new List<string> { "Server sid Error" });

            }

        }
    }
}
