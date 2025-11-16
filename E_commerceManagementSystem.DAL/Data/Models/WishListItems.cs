namespace E_commerceManagementSystem.DAL.Data.Models
{
    public class WishListItems
    {

        public int Id { get; set; }  // Primary Key
        public int WishListId { get; set; }  // Foreign Key referencing WishList

        public virtual WishList WishList { get; set; }  // Navigation property for the wishlist

        public int ProductId { get; set; }  // Foreign Key referencing Product
        public virtual Product Product { get; set; }  // Navigation property for the product

        public DateTime AddedDate { get; set; } = DateTime.UtcNow;  // Timestamp for when the item was added to the wishlist

    }
}
