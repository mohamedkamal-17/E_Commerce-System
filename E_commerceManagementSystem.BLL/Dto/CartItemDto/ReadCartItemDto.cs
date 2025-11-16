namespace E_commerceManagementSystem.BLL.Dto.CartItemDto
{
    public class ReadCartItemDto
    {
        public int Id { get; set; }
        public int CartID { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }

    }
}
