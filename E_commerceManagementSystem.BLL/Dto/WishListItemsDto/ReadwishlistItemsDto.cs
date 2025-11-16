namespace E_commerceManagementSystem.BLL.Dto.WishListItemsDto
{
    public class ReadwishlistItemsDto
    {
        public int Id { get; set; }  // Primary Key
        public int WishListId { get; set; }  // Foreign Key referencing WishList
        public int ProductId { get; set; }  // Foreign Key referencing Product
        public DateTime AddedDate { get; set; }
    }
}
