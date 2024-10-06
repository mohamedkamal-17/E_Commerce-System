using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.DAL.Data.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime? PaymentDate { get; set; }
        public double PaymentAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }

        public int OrderId { get; set; }          //Foreign Key  // one to one reltion
        public Order Orders { get; set; } // Navigation prop
    }
}
