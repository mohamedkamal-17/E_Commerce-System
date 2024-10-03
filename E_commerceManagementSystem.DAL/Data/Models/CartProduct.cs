using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.DAL.Data.Models
{
    public class CartProduct
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int ShoppingCartID { get; set; }
        public Cart ShoppingCart { get; set; }

    }
}
