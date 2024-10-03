using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.DAL.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public int SKU { get; set; }
        public string ImageURL { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int CategoryId { get; set; } // Foreign Key
        public Category category{ get; set; } // Navigation property
        public ICollection<OrderItem> orderItems { get; set; } = new HashSet<OrderItem>();// Navigation property
        public ICollection<CartProduct> ShoppingCartProduct { get; set; } = new HashSet<CartProduct>();// Navigation property
        public Inventory inventory { get; set; } // Navigation property
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>(); // Navigation property

        public ICollection<WishList> WishList { get; set; } = new HashSet<WishList>();

    }
}
