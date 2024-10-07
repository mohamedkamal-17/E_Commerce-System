using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.DAL.Data.Models
{
    public class Shipping
    {
        public int Id { get; set; } // Primary Key

        public int OrderId { get; set; } // Foreign Key referencing Orders
        public Order Order { get; set; } // Navigation property

        public string UserId { get; set; } // Foreign Key referencing Orders
        public ApplicationUser User { get; set; } // Navigation property

        public string ShippingMethod { get; set; } // e.g., Standard, Express, etc.
        public int TrackingNumber { get; set; }
        public decimal ShippingCost { get; set; }

        public DateTime? ShippedDate { get; set; } // Nullable if not yet shipped
        public DateTime? ExpectedDeliveryDate { get; set; } // Nullable if not yet shipped
        public string ShippingStatus { get; set; } // e.g., Shipped, In Transit, Delivered


    }
}
