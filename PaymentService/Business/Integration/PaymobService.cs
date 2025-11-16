using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace PaymentService.Business.PaymobServices
{
    public class PaymobClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly int _integrationId;

        public PaymobClient(int integrationId, HttpClient httpClient = null, string apiKey = null)
        {
            _integrationId = integrationId;
            _httpClient = httpClient;
            _apiKey = apiKey;
        }

        //get token 
        public async Task<string> Authenticate()
        {
            var body = new { api_key = _apiKey };
            var response = await _httpClient.PostAsJsonAsync("https://accept.paymob.com/api/auth/tokens", body);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<JsonElement>();
            return json.GetProperty("token").GetString();
        }

        // create order in paymob and return order id  
        public async Task<int> CreateOrder(string token, double amount, int merchantOrderId)
        {
            var body = new
            {
                auth_token = token,
                delivery_needed = false,
                amount_cents = (int)(amount * 100),
                currency = "EGP",
                merchant_order_id = merchantOrderId,
                items = new object[] { }
            };

            var resp = await _httpClient.PostAsJsonAsync("https://accept.paymob.com/api/ecommerce/orders", body);
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadFromJsonAsync<JsonElement>();
            return json.GetProperty("id").GetInt32();      // id for order in paymob
        }


        //create payment key
        public async Task<string> CreatePaymentKey(string token,int orderId,double amount,string email, string firstName,string lastName,
            string phone)
        {
            var body = new
            {
                auth_token = token,
                amount_cents = (int)(amount * 100),
                expiration = 3600,
                order_id = orderId,
                billimg_data = new
                {
                    email = email,
                    firstName = firstName,
                    lastName = lastName,
                    phone_number = phone
                },
                currency = "EGP",
                integration_id = _integrationId
            };
            var resp = await _httpClient.PostAsJsonAsync("https://accept.paymob.com/api/acceptance/payment_keys", body);
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadFromJsonAsync<JsonElement>();
            return json.GetProperty("token").GetString();
        }

        //get iframe link
        public string GetIframeUrl(string paymentToken)
        {
            return $"https://accept.paymob.com/api/acceptance/iframes/{_integrationId}?payment_token={paymentToken}";
        }
    }
}
