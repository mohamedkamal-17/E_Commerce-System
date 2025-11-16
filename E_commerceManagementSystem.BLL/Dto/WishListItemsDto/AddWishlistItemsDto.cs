namespace E_commerceManagementSystem.BLL.Dto.WishListItemsDto
{
    public class AddWishlistItemsDto
    {
        public int WishListId { get; set; }  // Foreign Key referencing WishList
        public int ProductId { get; set; }
    }
}
