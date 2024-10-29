using System.ComponentModel.DataAnnotations;

namespace E_commerceManagementSystem.BLL.Dto.ProductDto
{
    public class AddProductDto
    {
        [Required]

        public string ProductName { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]

        public double Price { get; set; }
        [Required]

        public int? StockQuantity { get; set; }
        [Required]

        public string? ImageURL { get; set; }
        [Required]

        public int? CategoryId { get; set; }


    }
}
