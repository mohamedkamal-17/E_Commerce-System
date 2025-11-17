using E_commerceManagementSystem.BLL.Dto.PaymobWebhookDto;
using E_commerceManagementSystem.DAL.Data.Dphelper;
using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using PaymentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.PaymentWebhookManager
{
    public class PaymentWebhookManager : IPaymentWebhookManager
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<PaymentWebhookManager> _logger;
        private readonly PaymobSettings _settings;

        public PaymentWebhookManager(ApplicationDbContext db, ILogger<PaymentWebhookManager> logger, PaymobSettings settings)
        {
            _db = db;
            _logger = logger;
            _settings = settings;
        }

        //private readonly IOrderNotifier? _orderNotifier;
        public async Task<bool> HandlePaymobAsync(string rawBody, IHeaderDictionary headers, CancellationToken ct = default)
        {
            if (!headers.TryGetValue("X-Paymob-Signature", out var signatureHeader))
            {
                _logger.LogWarning("Paymob webhook missing signature header.");
                return false; // reject
            }

            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_settings.WebhookSecret));
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(rawBody));
            var computedHex = Convert.ToHexString(computedHash).ToLower();

            if (!string.Equals(computedHex, signatureHeader.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogWarning("Invalid Paymob webhook signature. computed={Computed} header={Header}", computedHex, signatureHeader);
                return false; // reject
            }

            var json = JsonDocument.Parse(rawBody);
            var root = json.RootElement;

            var payload = new PaymobWebhookDto { RawBody = root };

            try
            {
                if (root.TryGetProperty("obj", out var obj))
                {
                    // success field
                    if (obj.TryGetProperty("success", out var successProp) && successProp.ValueKind == JsonValueKind.True)
                        payload.Success = true;
                    else if (obj.TryGetProperty("success", out successProp) && successProp.ValueKind == JsonValueKind.False)
                        payload.Success = false;

                    // order.id (paymob order id)
                    if (obj.TryGetProperty("order", out var orderObj) && orderObj.TryGetProperty("id", out var orderIdProp))
                    {
                        if (orderIdProp.ValueKind == JsonValueKind.Number && orderIdProp.TryGetInt32(out var oid))
                            payload.PaymobOrderId = oid;
                    }

                    // transaction id (sometimes id is top-level in obj)
                    if (obj.TryGetProperty("id", out var txIdProp) && txIdProp.ValueKind == JsonValueKind.Number)
                    {
                        payload.PaymobTransactionId = txIdProp.GetInt32().ToString();
                    }

                    // amount_cents
                    if (obj.TryGetProperty("amount_cents", out var amountProp) && amountProp.ValueKind == JsonValueKind.Number)
                    {
                        payload.AmountCents = amountProp.GetDecimal();
                    }

                    // merchant_order_id (some webhooks include it)
                    if (obj.TryGetProperty("merchant_order_id", out var merchantProp) && merchantProp.ValueKind == JsonValueKind.Number)
                    {
                        if (merchantProp.TryGetInt32(out var mid)) payload.MerchantOrderId = mid;
                    }
                }
                else
                {
                    // fallback: direct properties
                    if (root.TryGetProperty("success", out var s2) && s2.ValueKind == JsonValueKind.True) payload.Success = true;
                    if (root.TryGetProperty("amount_cents", out var a2) && a2.ValueKind == JsonValueKind.Number) payload.AmountCents = a2.GetDecimal();
                    if (root.TryGetProperty("order", out var ord2) && ord2.ValueKind == JsonValueKind.Object && ord2.TryGetProperty("id", out var id2) && id2.TryGetInt32(out var idx)) payload.PaymobOrderId = idx;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed parsing paymob webhook JSON");
                // we can still try to store raw body for investigation
            }

            // 3) Map paymob order id to our Payment entity.
            //    Prefer merchant_order_id if provided (it links to our orderId). Otherwise try to map by GatewayOrderId (paymob order id).
            Payment? payment = null;

            if (payload.MerchantOrderId.HasValue)
            {
                // merchant_order_id is our Order.Id; find Payment record by OrderId + GatewayName=Paymob
                payment = await _db.Payments.FirstOrDefaultAsync(p => p.OrderId == payload.MerchantOrderId.Value && p.GatewayName == "Paymob", ct);
            }

            if (payment == null && payload.PaymobOrderId.HasValue)
            {
                // If we stored the paymob order id earlier in Payment.GatewayOrderId, match against it
                var paymobOrderStr = payload.PaymobOrderId.Value.ToString();
                payment = await _db.Payments.FirstOrDefaultAsync(p => p.GatewayOrderId == paymobOrderStr && p.GatewayName == "Paymob", ct);
            }

            if (payment == null)
            {
                _logger.LogWarning("Webhook received but payment not found. paymobOrderId={PaymobId} merchantOrderId={MerchantId}", payload.PaymobOrderId, payload.MerchantOrderId);
                // Option: create a record or return 404. We'll return false to indicate not processed.
                return false;
            }


            // 4) Idempotency: check if we already processed the same gateway transaction id

            if (!string.IsNullOrEmpty(payload.PaymobTransactionId))
            {
                var exists = await _db.Transactions.AnyAsync(t => t.GatewayTransactionId == payload.PaymobTransactionId && t.GatewayName == "Paymob", ct);
                if (exists)
                {
                    _logger.LogInformation("Duplicate webhook received for transaction {TxId}", payload.PaymobTransactionId);
                    return true; // already processed -> OK
                }
            }


            // 5) Update payment status and add transaction record
            //    We will save raw JSON into GatewayRawResponse for debugging
            payment.GatewayRawResponse = rawBody;
            payment.UpdatedAt = DateTime.UtcNow;

            // Map status
            payment.Status = payload.Success ? PaymentStatus.Completed : PaymentStatus.Failed;
            if (payload.Success) payment.CompletedAt = DateTime.UtcNow;

            // Add transaction record representing this callback
            var tx = new Transaction
            {
                PaymentId = payment.Id,
                Amount = payment.Amount,
                Currency = payment.Currency,
                GatewayName = "Paymob",
                GatewayTransactionId = payload.PaymobTransactionId,
                GatewayRawResponse = rawBody,
                Type = payload.Success ? TransactionType.Charge : TransactionType.Authorization,
                Status = payload.Success ? TransactionStatus.Success : TransactionStatus.Failed,
                ProcessedAt = DateTime.UtcNow
            };

            _db.Transactions.Add(tx);

            // Optionally set LastTransaction reference on payment
            payment.LastTransactionId = tx.TransactionId; // Note: will be 0 until saved; you can save first then update if needed.

            await _db.SaveChangesAsync(ct);





            _logger.LogInformation("Processed Paymob webhook for payment {PaymentId}, success={Success}", payment.Id, payload.Success);
            return true;
        }
    }
}
