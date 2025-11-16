using E_commerceManagementSystem.BLL.Dto.OrederItemDto;

namespace E_commerceManagementSystem.BLL.Dto.OrderDto
{
    public class ReadOrderDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public double? TotalPrice { get; set; }
        public string Address { get; set; }
        public DateTime? ShippingDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public List<ReadOrderItemDto> OrderItems { get; set; }

    }
}
