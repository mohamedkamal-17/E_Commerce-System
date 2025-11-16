using System.Text.Json.Serialization;

namespace E_commerceManagementSystem.DAL.Data.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int CartID { get; set; }
        [JsonIgnore]
        public Cart Cart { get; set; }
        public bool IsDeleted { get; set; } = false;


    }
}
