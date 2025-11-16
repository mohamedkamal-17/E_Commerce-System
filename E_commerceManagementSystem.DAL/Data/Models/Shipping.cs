namespace E_commerceManagementSystem.DAL.Data.Models
{
    public class Shipping
    {
        public int Id { get; set; } // Primary Key

        public int OrderId { get; set; } // Foreign Key referencing Orders
        public Order Order { get; set; } // Navigation property

        public string UserId { get; set; } // Foreign Key referencing Orders
        public ApplicationUser User { get; set; } // Navigation property
        public bool IsDeleted { get; set; } = false;

        public string ShippingMethod { get; set; } = "Care"; // e.g., Standard, Express, etc.
        public string TrackingNumber { get; set; } = Guid.NewGuid().ToString();
        public decimal ShippingCost { get; set; } = 60m;

        public DateTime? ShippedDate { get; set; } = DateTime.Now; // Nullable if not yet shipped
        public DateTime? ExpectedDeliveryDate { get; set; } = DateTime.Now.AddDays(10); // Nullable if not yet shipped
        public string ShippingStatus { get; set; } // e.g., Shipped, In Transit, Delivered


    }
}
