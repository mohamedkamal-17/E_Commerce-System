using E_commerceManagementSystem.BLL.Dto.CartItemDto;

namespace E_commerceManagementSystem.BLL.Dto.CartDto
{
    public class ReadCartDto
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public ICollection<ReadCartItemDto>? CartItems { get; set; }

    }
}
