namespace PaymentService
{
    public class PaymobSettings
    {
        public string ApiKey { get; set; }
       public string IntegrationId          { get; set; }
       public string IframeId               { get; set; }
       public string BaseUrl { get; set; } = "https://accept.paymob.com";
        public string HmacSecret { get; set; }
    }
}
