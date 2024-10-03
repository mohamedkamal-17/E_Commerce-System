using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.DAL.Data.Models
{
    public class WishList
    {
        public int Id { get; set; } // Primary Key

        public string UserId { get; set; } // Foreign Key referencing Users
        public ApplicationUser User { get; set; } // Navigation property

        public int ProductID { get; set; } // Foreign Key referencing Products
        public Product Product { get; set; } // Navigation property

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp
    }
}
