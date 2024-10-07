using E_commerceManagementSystem.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Dto.OrderDto
{
    public class ReadOrderDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public double? TotalPrice { get; set; }
        public string Address { get; set; }
        public DateTime? ShippingDate { get; set; } 
        public DateTime? ArrivalDate { get; set; }

    }
}
