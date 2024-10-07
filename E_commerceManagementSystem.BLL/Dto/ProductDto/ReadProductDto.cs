using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Dto.ProductDto
{
    public class ReadProductDto
    {
       
            public int Id { get; set; } // Include ID for reference if needed
            public string ProductName { get; set; }
            public string Description { get; set; }
            public double Price { get; set; }
            public string? ImageURL { get; set; } 
        
    }
}
