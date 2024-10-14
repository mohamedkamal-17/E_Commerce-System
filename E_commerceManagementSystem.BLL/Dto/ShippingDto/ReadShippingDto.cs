using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Dto.ShippingDto
{
    public class ReadShippingDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int OrderId { get; set; } // Foreign Key referencing Orders
        public string ShippingStatus { get; set; }

        public string ShippingMethod { get; set; } // e.g., Standard, Express, etc.
        public string TrackingNumber { get; set; }
        public decimal ShippingCost { get; set; }
    }
}
