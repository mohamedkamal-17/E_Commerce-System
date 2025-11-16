namespace E_commerceManagementSystem.DAL.Data.Models
{
    public class Inventory
    {
        public bool IsDeleted { get; set; } = false;
        public int Id { get; set; } // Primary Key
        public int StockQuantity { get; set; }
        public int ReorderLevel { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int ProductId { get; set; } // Foreign Key referencing Product
        public Product Product { get; set; } // Navigation property
    }
}
