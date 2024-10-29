namespace E_commerceManagementSystem.BLL.Dto.PaymentDto
{
    public class ReadPaymentDto
    {
        public int Id { get; set; }               // Payment ID (Primary Key)
        public decimal TotalAmount { get; set; }  // Total amount of the payment
        public string Currency { get; set; }      // Currency used for the payment
        public string Status { get; set; }        // Payment status (e.g., succeeded, pending)
        public string PaymentIntentId { get; set; } // Stripe Payment Intent ID
        public DateTime CreatedAt { get; set; }   // Timestamp when the payment was created
        public DateTime UpdatedAt { get; set; }   // Timestamp when the payment was last updated

    }
}
