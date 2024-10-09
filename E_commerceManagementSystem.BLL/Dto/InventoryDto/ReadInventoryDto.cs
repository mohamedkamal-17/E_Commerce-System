using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Dto.InventoryDto
{
    public class ReadInventoryDto
    {
        public int Id { get; set; } // Primary Key
        public int StockQuantity { get; set; }
        public int ReorderLevel { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int ProductId { get; set; } // Foreign Key referencing Product

    }
}
