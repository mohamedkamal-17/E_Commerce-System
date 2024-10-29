namespace E_commerceManagementSystem.DAL.Data.Models
{
    public class Review
    {
        public bool IsDeleted { get; set; } = false;

        public int Id { get; set; } // (Primary Key) 
        public int Rating { get; set; } // (1-5) Scale
        public string ReviewText { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public int ProductId { get; set; } //Foreign Key referencing Product
        public virtual Product Product { get; set; } //Navigation prop
        public string UserId { get; set; } //(Foreign Key referencing ApplicationUser) 
        public virtual ApplicationUser User { get; set; } //Navigation prop


    }
}
