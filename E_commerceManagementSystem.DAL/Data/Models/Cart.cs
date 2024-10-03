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
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string UserId { get; set; } //(Foreign Key referencing ApplicationUser)
        public ApplicationUser User { get; set; } // Navigation property
        public int ProductId { get; set; } // (Foreign Key referencing Product)
        public ICollection<CartProduct> ShoppingCartProduct { get; set; } // Navigation property


    }
}
