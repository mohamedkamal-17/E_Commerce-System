namespace E_commerceManagementSystem.DAL.Data.Models
{
    public class OrderItem
    {
        public bool IsDeleted { get; set; } = false;

        public int Id { get; set; } // Primary Key
        public int OrderId { get; set; } // Foreign Key referencing Order
        public int ProductId { get; set; } // Foreign Key referencing Product
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
