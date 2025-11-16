using E_commerceManagementSystem.DAL.Data.Models;

namespace PaymentService.Business
{
    public interface IPaymentManager
    {
        Task<(Payment payment, string? redirectUrl)> CreatePaymentAsync(int orderId, string userId, decimal amount, string currency, string? email, string? firstName, string? lastName, string? phone, string? paymentMethod);
        Task<Payment?> GetPaymentAsync(int id);
        Task HandleGatewayCallbackAsync(PaymobCallbackModel model);
    }
}
