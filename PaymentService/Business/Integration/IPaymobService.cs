using E_commerceManagementSystem.DAL.Data.Models;

namespace PaymentService.Business.PaymobServices
{
    public interface IPaymobService
    {
        Task<PaymobCreateResult?> CreatePaymentAsync(Payment payment);
        Task<bool> ValidateHmacAsync(string payload, string hmacHeader);
    }
}
