using E_commerceManagementSystem.DAL.Data.Dphelper;
using Grpc.Core;
using Microsoft.Extensions.Options;
using PaymentService.Business.PaymobServices;
using PaymentService.DTOs;
using E_commerceManagementSystem.DAL.Data.Models;


namespace PaymentService.Services
{
    public class PaymentService : PaymentGrpc.PaymentGrpcBase
    {
        private readonly PaymobClient _paymob;        
        private readonly ILogger<PaymentService> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly PaymobSettings _paymobSettings;
        public PaymentService(ILogger<PaymentService> logger, ApplicationDbContext db,
            IHttpClientFactory httpClientFactory,
            IOptions<PaymobSettings> paymobOptions, PaymobClient paymob)
        {
            _logger = logger;
            _db = db;
            _httpClientFactory = httpClientFactory;
            _paymobSettings = paymobOptions.Value;
            _paymob = paymob;
        }

        public override async Task<CreatePaymentResponse> CreatePayment(CreatePaymentRequest request, ServerCallContext context)
        {

            //get token to use paymob APIs
            string token = await _paymob.Authenticate();

            int paymobOrderId = await _paymob.CreateOrder(token, request.Amount,request.OrderId);

            string paymentToken = await _paymob.CreatePaymentKey(
                token,
                paymobOrderId,
                request.Amount,
                request.CustomerEmail,
                request.CustomerFirstName,
                request.CustomerLastName,
                request.CustomerPhone);


            string redirectUrl = _paymob.GetIframeUrl(paymentToken);


           
            Payment payment = new Payment
            {
                OrderId = request.OrderId,
                Amount = (decimal)request.Amount,
                Currency = request.Currency,

                GatewayName = "Paymob",
                SelectedPaymentMethod = request.PaymentMethod,
                GatewayOrderId = paymobOrderId.ToString(),
                GatewayPaymentToken = paymentToken,
                GatewayPaymentUrl = redirectUrl,

                CustomerEmail = request.CustomerEmail,
                CustomerFirstName = request.CustomerFirstName,
                CustomerLastName = request.CustomerLastName,
                CustomerPhone = request.CustomerPhone,

                GatewayRawResponse = null
            };

            _db.Payments.Add(payment);
            await _db.SaveChangesAsync();


            //var client = _httpClientFactory.CreateClient();
            //var orderRequest = new PaymobCreateOrderRequest
            //{
            //    auth_token = _paymobSettings.ApiKey,
            //    delivery_needed = false,
            //    amount_cents = (int)(payment.Amount * 100),
            //    currency = payment.Currency,
            //    merchant_order_id = payment.Id
            //};

            //var orderResponse = await client.PostAsJsonAsync($"{_paymobSettings.BaseUrl}/ecommerce/orders", orderRequest);
            //var orderData = await orderResponse.Content.ReadFromJsonAsync<PaymobCreateOrderResponse>();

            //var paymentKeyRequest = new PaymobPaymentKeyRequest
            //{
            //    auth_token = _paymobSettings.ApiKey,
            //    amount_cents = (int)(payment.Amount * 100),
            //    order_id = orderData.id,
            //    billing_data_email = payment.CustomerEmail,
            //    currency = payment.Currency,
            //    integration_id = int.Parse(_paymobSettings.IntegrationId)
            //};

            //var paymentKeyResponse = await client.PostAsJsonAsync($"{_paymobSettings.BaseUrl}/acceptance/payment_keys", paymentKeyRequest);
            //var paymentKeyData = await paymentKeyResponse.Content.ReadFromJsonAsync<PaymobPaymentKeyResponse>();

            //payment.GatewayOrderId = orderData.id.ToString();
            //payment.GatewayPaymentUrl=PaymobHelper.GetIframeUrl(_paymobSettings.IframeId,paymentKeyData.token); //
            //await _db.SaveChangesAsync();


            var transaction = new Transaction
            {
                PaymentId = payment.Id,
                Amount = payment.Amount,
                Currency = payment.Currency,
                GatewayName = "Paymob",
                GatewayTransactionId = null,
                Type = TransactionType.Authorization,
                Status = TransactionStatus.Pending,
                ProcessedAt = DateTime.UtcNow,
                Metadata = null
            };

            _db.Transactions.Add(transaction);
            await _db.SaveChangesAsync();


            //return response to grpc client
            return new CreatePaymentResponse
            {
                PaymentId = payment.Id,
                Status = payment.Status.ToString(),
                RedirectUrl = redirectUrl,
                GatewayOrderId = paymobOrderId.ToString(),
                PaymentToken = paymentToken,
            };
        }



    }
}
