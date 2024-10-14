using E_commerceManagementSystem.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Dto.OrederItemDto
{
    public class ReadOrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; } 
        public int ProductId { get; set; } 
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
    }
}
