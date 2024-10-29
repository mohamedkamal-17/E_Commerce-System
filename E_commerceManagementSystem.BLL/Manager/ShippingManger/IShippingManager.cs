using E_commerceManagementSystem.BLL.Dto.ShippingDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
namespace E_commerceManagementSystem.BLL.Manager.ShippingManger
{
    public interface IShippingManager : IManager<Shipping, ReadShippingDto, AddShippingDto, UpdateShippingDto>
    {

        Task<GeneralRespons> GetByOrderIdAsync(int id);
        Task<GeneralRespons> GetByTrackingNumberAsync(string trackingNumber);
        Task<GeneralRespons> GetByShippingStateAsync(string shippingStatus);
        Task<GeneralRespons> GetByShippingDateAsync(DateTime shippingDate);

    }
}
