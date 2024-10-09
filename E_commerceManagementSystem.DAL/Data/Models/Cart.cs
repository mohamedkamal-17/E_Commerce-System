using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.DAL.Data.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        public string UserId { get; set; } //(Foreign Key referencing ApplicationUser)
        public virtual ApplicationUser User { get; set; } // Navigation property
        public virtual ICollection<CartItem>? CartItems { get; set; } // Navigation property


    }
}
