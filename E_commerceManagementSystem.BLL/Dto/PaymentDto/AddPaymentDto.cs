namespace E_commerceManagementSystem.BLL.Dto.PaymentDto
{
    public class AddPaymentDto
    {
        public decimal? TotalAmount { get; set; }  // Total amount for the payment
        public string Currency { get; set; }      // Currency for the payment (e.g., USD, EUR)
        public string PaymentMethodId { get; set; } // Stripe Payment Method ID
        public int OrderId { get; set; }          // The order ID associated with this payment



    }
}
