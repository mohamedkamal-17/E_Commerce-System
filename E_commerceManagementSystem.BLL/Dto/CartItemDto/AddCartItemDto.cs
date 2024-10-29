using System.ComponentModel.DataAnnotations;

namespace E_commerceManagementSystem.BLL.Dto.CartItemDto
{
    public class AddCartItemDto
    {
        [Required]
        public int Quantity { get; set; }
        [Required]

        public int ProductId { get; set; }
        [Required]

        public int CartID { get; set; }
    }
}
