using E_commerceManagementSystem.BLL.Dto.WishListItemsDto;

namespace E_commerceManagementSystem.BLL.Dto.WishlistDto
{
    public class ReadWishlistDto
    {
        public int Id { get; set; }  // Primary Key
        public string UserId { get; set; }  // Foreign Key referencing ApplicationUser
        public DateTime CreatedDate { get; set; }  // Timestamp for when the wishlist was created
        public DateTime UpdatedDate { get; set; }  // Timestamp for when the wishlist was last updated
        public List<ReadwishlistItemsDto> WishListItems { get; set; }
    }
}
