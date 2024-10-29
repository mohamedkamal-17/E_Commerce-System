namespace E_commerceManagementSystem.DAL.Data.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string PaymentIntentId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public double TotalAmount { get; set; }
        public bool IsDeleted { get; set; } = false;

        public string? PaymentMethod { get; set; }
        public string Status { get; set; }
        public string Currency { get; set; }
    }
}
