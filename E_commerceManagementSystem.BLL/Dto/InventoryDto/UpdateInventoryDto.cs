using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Dto.InventoryDto
{
    public class UpdateInventoryDto
    {
        public int StockQuantity { get; set; }
        public int ReorderLevel { get; set; }
    }
}
