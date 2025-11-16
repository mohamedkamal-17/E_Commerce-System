namespace PaymentService.DTOs
{
    public class PaymobPaymentKeyRequest
    {
        public string auth_token { get; set; } = string.Empty;
        public int amount_cents { get; set; }
        public string expiration { get; set; } = "3600";// sec
        public int order_id { get; set; }
        public string billing_data_email { get; set; } = string.Empty;
        public string currency { get; set; } = "EGP";
        // card integration
        public int integration_id { get; set; }
    }
}
