using E_commerceManagementSystem.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Dto.ShippingDto
{
    public class AddShippingDto
    {
       
        public string UserId { get; set; }
        public int OrderId { get; set; } // Foreign Key referencing Orders
       
      
      
     
    }
}
