using System.Text.Json.Serialization;

namespace E_commerceManagementSystem.DAL.Data.Models
{
    public class Product
    {
        public bool IsDeleted { get; set; } = false;
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int? StockQuantity { get; set; }
        public string? ImageURL { get; set; }
        public DateTime? CreatedAt { get; set; }// = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public int? CategoryId { get; set; } // Foreign Key
        public Category? Category { get; set; } // Navigation property
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();// Navigation property

        [JsonIgnore]
        public ICollection<CartItem> CartItem { get; set; } = new HashSet<CartItem>();// Navigation property
        public int? InventoryId { get; set; }
        public Inventory? Inventory { get; set; } // Navigation property
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>(); // Navigation property

        public ICollection<WishListItems> WishList { get; set; } = new HashSet<WishListItems>();

    }
}
