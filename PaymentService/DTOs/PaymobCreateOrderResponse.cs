namespace PaymentService.DTOs
{
    public class PaymobCreateOrderResponse
    {
        public int id { get; set; }                  //   order id in  Paymob
        public string token { get; set; } = string.Empty; //payment token
        public string? status { get; set; }         // pending completed failed
    }
}
