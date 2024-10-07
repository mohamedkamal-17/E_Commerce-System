using E_commerceManagementSystem.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Dto.CartItemDto
{
    public class AddCartItemDto
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int CartID { get; set; }
    }
}
