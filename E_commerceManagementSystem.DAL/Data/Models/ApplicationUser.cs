using Microsoft.AspNetCore.Identity;

namespace E_commerceManagementSystem.DAL.Data.Models
{
    public class ApplicationUser : IdentityUser
    {


        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<Order>? Orders { get; set; } // Nvigation prop
        public Cart? ShoppingCart { get; set; }  // Nvigation prop
        public ICollection<Review>? Reviews { get; set; } // Nvigation prop

        public ICollection<WishList>? WishList { get; set; }
        public ICollection<Shipping>? Shipping { get; set; }

    }
}
