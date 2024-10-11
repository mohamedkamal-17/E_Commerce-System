using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.DAL.Data.Models
{
    public class CartItem
    {
        public int Id { get; set; } 
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int CartID { get; set; }
        public Cart Cart { get; set; }

    }
}
