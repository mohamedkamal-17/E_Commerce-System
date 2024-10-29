namespace E_commerceManagementSystem.BLL.Dto.ReviewDto
{
    public class ReadReviewDto
    {
        public int Id { get; set; }                // Unique identifier for the review (Primary Key)
        public int Rating { get; set; }            // Rating given by the user (1-5 scale)
        public string ReviewText { get; set; }     // The actual text of the review
        public DateTime? CreatedDate { get; set; } = DateTime.Now; // When the review was created (default to now)
        public int ProductId { get; set; }         // Foreign Key referencing Product
        public string ProductName { get; set; }    // Name of the product being reviewed
        public string UserId { get; set; }         // Foreign Key referencing ApplicationUser
        public string UserName { get; set; }


    }
}
