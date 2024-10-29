using System.ComponentModel.DataAnnotations;

namespace E_commerceManagementSystem.BLL.Dto.OrderDto
{
    public class AddOrderDto
    {
        [Required]
        public int CartId { get; set; }
        [Required]

        public string Address { get; set; }
        [Required]

        public int PaymentId { get; set; }

    }
}
