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
        public string Status { get; set; }
        public double TotalPrice { get; set; }
        public string Address { get; set; }
        public DateTime? ShippingDate { get; set; }  = DateTime.Now;
        public DateOnly? ArrivalDate { get; set; }
        public int PaymentId { get; set; }
        public ApplicationUser User { get; set; } // Navigation prop
        public ICollection<OrderItem> OrderItems { get; set; }  // Navigation prop
        public Payment? Payment { get; set; } // Navigation prop

        public Shipping Shipping { get; set; }
    }
}
