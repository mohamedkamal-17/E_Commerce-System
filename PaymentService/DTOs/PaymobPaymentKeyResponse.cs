namespace PaymentService.DTOs
{
    public class PaymobPaymentKeyResponse
    {
        public string token { get; set; } = string.Empty;  //use it in  iframe URL
        public string? expires_at { get; set; }
    }
}
