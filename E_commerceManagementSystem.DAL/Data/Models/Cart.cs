using System.Text.Json.Serialization;

namespace E_commerceManagementSystem.DAL.Data.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;

        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        public string UserId { get; set; } //(Foreign Key referencing ApplicationUser)
        [JsonIgnore]
        public virtual ApplicationUser User { get; set; } // Navigation property
        public virtual ICollection<CartItem>? CartItems { get; set; } // Navigation property


    }
}
