using AutoMapper;
using AutoMapper.QueryableExtensions; // Import for ProjectTo
using E_commerceManagementSystem.BLL.Dto.PaymentDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.PaymebtRepository;
using Stripe;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using E_commerceManagementSystem.BLL.Manager.OrderManager;
using E_commerceManagementSystem.DAL.Reposatories.OrederRepository;
using Microsoft.Extensions.Configuration;

namespace E_commerceManagementSystem.BLL.Manager.PaymentManager
{
    public class PaymentManager : Manager<Payment, ReadPaymentDto, AddPaymentDto, UpdatePaymentDto>, IPaymentManger
    {
        private readonly IPaymentRepo _paymentRepo;
        private readonly IMapper _mapper;
        private readonly IOrderRepo _orderRepo;
        private readonly IConfiguration _configuration;

        public PaymentManager(IPaymentRepo paymentRepo, IMapper mapper, IOrderRepo orderRepo, IConfiguration configuration)
            : base(paymentRepo, mapper)
        {
            _paymentRepo = paymentRepo;
            _mapper = mapper;
            _orderRepo = orderRepo;
            _configuration = configuration;
        }

        public async Task<GeneralRespons> ProcessPaymentAsync(AddPaymentDto dto)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            try
            {
                var intent = await CreatePaymentIntentAsync(dto);
                var payment = _mapper.Map<Payment>(dto);
                payment.PaymentIntentId = intent.Id;
                payment.Status = intent.Status;
                payment.CreatedAt = DateTime.UtcNow;
                payment.UpdatedAt = DateTime.UtcNow;

                await SavePaymentAsync(payment);
                await UpdateOrderWithPaymentDetailsAsync(dto.OrderId, payment, intent);

                return CreateResponse(true, _mapper.Map<AddIntentPymentRsponsDto>(intent), "Payment processed successfully.", 200);
            }
            catch (StripeException e)
            {
                return CreateResponse(false, null, e.Message, 500); // Internal Server Error
            }
        }

        public async Task<GeneralRespons> ConfirmPaymentAsync(string paymentIntentId)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            try
            {
                // Confirm the payment intent
                var intent = await ConfirmPaymentIntentAsync(paymentIntentId);
                if (intent != null && intent.Status == "succeeded") // assuming 'succeeded' is the successful status
                {
                    // Update the payment status in the system
                    await UpdatePaymentStatusAsync(paymentIntentId, intent);

                    // Update the order status based on the payment intent
                    await UpdateOrderStatusBasedOnPaymentAsync(paymentIntentId, intent);

                    // Return success response with mapped data
                    return CreateResponse(true, _mapper.Map<AddIntentPymentRsponsDto>(intent), "Payment confirmed successfully.", 200);
                }
                else
                {
                    // Handle unsuccessful payment confirmation
                    return CreateResponse(false, null, "Payment confirmation failed.", 400); // Bad Request
                }
            }
            catch (StripeException e)
            {
                // Handle Stripe-specific exceptions
                return CreateResponse(false, null, $"Stripe Error: {e.Message}", 500); // Internal Server Error
            }
            catch (Exception ex)
            {
                // Catch any other exceptions and return server error
                return CreateResponse(false, null, $"Server-side Error: {ex.Message}", 500); // Internal Server Error
            }
        }

        private async Task<PaymentIntent> CreatePaymentIntentAsync(AddPaymentDto dto)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(dto.TotalAmount * 100),
                Currency = dto.Currency,
                PaymentMethod = dto.PaymentMethodId,
                ConfirmationMethod = "manual",
                Confirm = false,
            };

            var service = new PaymentIntentService();
            return await service.CreateAsync(options);
        }

        private async Task SavePaymentAsync(Payment payment)
        {
            await _paymentRepo.AddAsync(payment);
        }

        private async Task UpdateOrderWithPaymentDetailsAsync(int orderId, Payment payment, PaymentIntent intent)
        {
            var order = await _orderRepo.GetByIdAsync(orderId);
            if (order != null)
            {
                order.PaymentId = payment.Id;
                order.PaymentIntentId = intent.Id;
                order.Status = "Pending";
                await _orderRepo.UpdateAsync(order);
            }
        }

        private GeneralRespons CreateResponse(bool success, object? model, string message, int statusCode, List<string>? errors = null)
        {
            return new GeneralRespons
            {
                Success = success,
                Model = model,
                Message = message,
                StatusCode = statusCode,
                Errors = errors ?? new List<string>()
            };
        }

        private async Task<PaymentIntent> ConfirmPaymentIntentAsync(string paymentIntentId)
        {
            var service = new PaymentIntentService();
            return await service.ConfirmAsync(paymentIntentId);
        }

        private async Task UpdatePaymentStatusAsync(string paymentIntentId, PaymentIntent intent)
        {
            var payment = await _paymentRepo.GetByConditionAsync(p => p.PaymentIntentId == paymentIntentId)
                                             .ProjectTo<Payment>(_mapper.ConfigurationProvider)
                                             .FirstOrDefaultAsync();
            if (payment != null)
            {
                payment.Status = intent.Status;
                payment.UpdatedAt = DateTime.UtcNow;
                await _paymentRepo.SaveChangesAsync();
            }
        }

        private async Task UpdateOrderStatusBasedOnPaymentAsync(string paymentIntentId, PaymentIntent intent)
        {
            var ordersResult = await _orderRepo.GetByConditionAsync(o => o.PaymentIntentId == paymentIntentId)
                                                .ProjectTo<Order>(_mapper.ConfigurationProvider)
                                                .ToListAsync();
            if (ordersResult.Any())
            {
                var order = ordersResult.First();
                order.Status = intent.Status == "succeeded" ? "Paid" : "Payment Failed";
                await _orderRepo.SaveChangesAsync();
            }
        }
    }
}
