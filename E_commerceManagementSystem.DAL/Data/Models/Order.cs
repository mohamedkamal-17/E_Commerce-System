using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.DAL.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; } //Foreign Key Referencing ApplicationUser 
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public double TotalAmount { get; set; }
        public int ShippingAddress { get; set; }
        public int BillingAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int paymentId { get; set; }
        public ApplicationUser User { get; set; } // Navigation prop
        public ICollection<OrderItem> orderItems { get; set; }  // Navigation prop
        public Payment payment { get; set; } // Navigation prop

        public Shipping Shipping { get; set; }
    }
}
