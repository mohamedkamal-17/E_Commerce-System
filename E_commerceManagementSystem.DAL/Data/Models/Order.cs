namespace E_commerceManagementSystem.DAL.Data.Models
{
    public class Order
    {
        public bool IsDeleted { get; set; } = false;
        public int Id { get; set; }
        public string? PaymentIntentId { get; set; }
        public string UserId { get; set; } //Foreign Key Referencing ApplicationUser 
        public string Status { get; set; } = "Pending";
        public double? TotalPrice { get; set; }
        public string? Address { get; set; }
        public DateTime? ShippingDate { get; set; } = DateTime.Now;
        public DateTime? ArrivalDate { get; set; } = DateTime.Now.AddDays(10);
        public int PaymentId { get; set; }
        public virtual ApplicationUser? User { get; set; } // Navigation prop
        public virtual ICollection<OrderItem>? OrderItems { get; set; }  // Navigation prop
        public Payment? Payment { get; set; } // Navigation prop

        public Shipping? Shipping { get; set; }
    }
}
