using System.ComponentModel.DataAnnotations;

namespace E_commerceManagementSystem.BLL.Dto.OrederItemDto
{
    public class AddOrderItemDto
    {
        [Required]

        public int ProductId { get; set; }
        [Required]

        public int Quantity { get; set; }
    }
}
