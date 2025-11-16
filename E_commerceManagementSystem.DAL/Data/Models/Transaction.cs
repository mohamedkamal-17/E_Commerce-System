using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.DAL.Data.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        // Link to payment
        public int PaymentId { get; set; }
        public Payment Payment { get; set; } = null!;

        // Gateway data - Works with ANY payment provider
        public string GatewayName { get; set; } = string.Empty;  // Which gateway processed this
        public string? GatewayTransactionId { get; set; }  // Gateway's transaction ID
        public string GatewayRawResponse { get; set; } = string.Empty;  // Full JSON response

        // Transaction details
        public TransactionType Type { get; set; }
        public TransactionStatus Status { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "EGP";

        // Payment method details (generic across gateways)
        public string? MaskedCardNumber { get; set; }  // e.g., "**** **** **** 1234"
        public string? PaymentMethod { get; set; }  // card, wallet, bank_transfer
        public string? CardBrand { get; set; }  // "Visa", "Mastercard", "Mada", etc.
        public string? ErrorMessage { get; set; }
        public string? ErrorCode { get; set; }

        // Metadata - For gateway-specific additional data
        public string? Metadata { get; set; } 
        public DateTime ProcessedAt { get; set; } = DateTime.UtcNow;
    }

    public enum TransactionType
    {
        Authorization = 1,  // Hold funds (not captured yet)
        Charge = 2,         // Capture funds
        Refund = 3,         // Return money to customer
        Void = 4,           // Cancel authorization
        Chargeback = 5      // Customer disputed transaction
    }

    public enum TransactionStatus
    {
        Success = 1,
        Failed = 2,
        Pending = 3,
        Declined = 4  // Card declined, insufficient funds, etc.
    }

}
