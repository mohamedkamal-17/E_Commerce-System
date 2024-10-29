namespace E_commerceManagementSystem.BLL.Dto.ReviewDto
{
    public class AddReviewDto
    {
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
    }
}
