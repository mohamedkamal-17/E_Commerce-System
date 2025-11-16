namespace PaymentService
{
    public static class PaymobHelper
    {
        public static string GetIframeUrl(string iframeId, string paymentToken)
        {
            return $"https://accept.paymob.com/api/acceptance/iframes/{iframeId}?payment_token={paymentToken}";
        }
    }
}
