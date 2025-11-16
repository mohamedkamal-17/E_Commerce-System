namespace PaymentService.DTOs
{
    public class PaymobCreateOrderRequest
    {
        public string auth_token { get; set; } = string.Empty;
        public bool delivery_needed { get; set; } = false;
        public int amount_cents { get; set; }
        public string currency { get; set; } = "EGP";
        public int merchant_order_id { get; set; }
    }
}
