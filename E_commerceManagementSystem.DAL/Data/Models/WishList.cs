namespace E_commerceManagementSystem.DAL.Data.Models
{
    public class WishList
    {
        public int Id { get; set; }  // Primary Key
        public string UserId { get; set; }  // Foreign Key referencing ApplicationUser

        public ApplicationUser User { get; set; }  // Navigation property for the user

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;  // Timestamp for when the wishlist was created
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;  // Timestamp for when the wishlist was last updated

        public ICollection<WishListItems> WishListItems { get; set; }  // Navigation property for the wishlist items

    }
}
