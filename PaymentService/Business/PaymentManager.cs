using E_commerceManagementSystem.DAL.Data.Models;
using PaymentService.Business.PaymobServices;
using PaymentService.Data.Repositories;

namespace PaymentService.Business
{
    public class PaymentManager : IPaymentManager
    {
        private readonly IPaymentRepository _repo;
        private readonly IPaymobService _paymob;


        public PaymentManager(IPaymentRepository repo, IPaymobService paymob)
        {
            _repo = repo;
            _paymob = paymob;
        }
        public async Task<(Payment payment, string? redirectUrl)> CreatePaymentAsync(int orderId, string userId, decimal amount, string currency, string? email, string? firstName, string? lastName, string? phone, string? paymentMethod)
        {
            var payment = new Payment
            {
                OrderId = orderId,
                Amount = amount,
                Currency = currency,
                GatewayName = "Paymob",
                CustomerEmail = email ?? string.Empty,
                CustomerFirstName = firstName ?? string.Empty,
                CustomerLastName = lastName ?? string.Empty,
                CustomerPhone = phone ?? string.Empty,
            };

            await _repo.AddAsync(payment);

            //call paymob to get redirect/url/token
            var res = await _paymob.ToString();


        }

        public Task<Payment?> GetPaymentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task HandleGatewayCallbackAsync(PaymobCallbackModel model)
        {
            throw new NotImplementedException();
        }
    }
}
