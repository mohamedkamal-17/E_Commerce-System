namespace E_commerceManagementSystem.BLL.Dto.ProductDto
{
    public class UpdateProductDto
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int? StockQuantity { get; set; }
        public string? ImageURL { get; set; }
        public int? CategoryId { get; set; }

    }
}
