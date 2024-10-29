namespace E_commerceManagementSystem.BLL.Dto.ShippingDto
{
    public class UpdateShippingDto
    {
        public int OrderId { get; set; } // Foreign Key referencing Orders

        public string ShippingMethod { get; set; } // e.g., Standard, Express, etc.
        public string TrackingNumber { get; set; }
        public decimal ShippingCost { get; set; }
    }
}
