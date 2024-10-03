using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace E_commerceManagementSystem.DAL.Data.Models
{
    public class ApplicationUser: IdentityUser
    {
       
        
        public DateTime? CreatedAt { get; set; }
        public DateTime?UpdatedAt { get; set; }

        public ICollection<Order>?  Orders { get; set; } // Nvigation prop
        public Cart? ShoppingCart { get; set; }  // Nvigation prop
        public ICollection<Review>? Reviews { get; set; } // Nvigation prop

        public ICollection<WishList>? WishList { get; set; }

    }
}
