using E_commerceManagementSystem.BLL.Dto.PaymentDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;

namespace E_commerceManagementSystem.BLL.Manager.PaymentManager
{
    public interface IPaymentManger : IManager<Payment, ReadPaymentDto, AddPaymentDto, UpdatePaymentDto>
    {
        Task<GeneralRespons> ProcessPaymentAsync(AddPaymentDto dto);
        Task<GeneralRespons> ConfirmPaymentAsync(string paymentIntentId);
    }
}
