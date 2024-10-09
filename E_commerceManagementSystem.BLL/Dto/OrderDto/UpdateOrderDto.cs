using E_commerceManagementSystem.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Dto.OrderDto
{ 
    public class UpdateOrderDto
    {
        public string Address { get; set; }
        public string Status { get; set; }
      
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

    }
}
