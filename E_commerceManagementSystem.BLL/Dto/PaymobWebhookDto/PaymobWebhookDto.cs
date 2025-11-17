using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Dto.PaymobWebhookDto
{
    public class PaymobWebhookDto
    {
        public JsonElement RawBody { get; set; }
        public bool Success { get; set; }
        public int? PaymobOrderId { get; set; }          
        public string? PaymobTransactionId { get; set; } 
        public decimal? AmountCents { get; set; }        
        public string? Currency { get; set; }
        public int? MerchantOrderId { get; set; }
    }
}
