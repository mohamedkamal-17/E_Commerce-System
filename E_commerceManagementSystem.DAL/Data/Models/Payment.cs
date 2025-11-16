using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.DAL.Data.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public decimal Amount { get; set; }
        public string Currency { get; set; } = "EGP";
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        // Gateway
        public int? LastTransactionId { get; set; }
        public Transaction? LastTransaction { get; set; }
        public string? GatewayName { get; set; }  // "Paymob", "Stripe"
        public string? SelectedPaymentMethod { get; set; } // card , wallet
        public string? GatewayOrderId { get; set; }  // Gateway's order reference
        public string? GatewayPaymentToken { get; set; }  // Payment token/key from gateway
        public string? GatewayPaymentUrl { get; set; }  // URL where customer pays
        public string? GatewayRawResponse { get; set; }  //gateway response as JSON
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerFirstName { get; set; } = string.Empty;
        public string CustomerLastName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string? Metadata { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }  // Payment links can expire
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    }
    public enum PaymentStatus
    {
        Pending = 1,
        Processing = 2,
        Completed = 3,
        Failed = 4,
        Cancelled = 5,
        Refunded = 6,
        Expired = 7  // Added: Payment link expired
    }
}
