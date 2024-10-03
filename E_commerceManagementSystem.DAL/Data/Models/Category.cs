using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.DAL.Data.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int ParentCategoryID { get; set; }
        public int CreatedAt { get; set; }
        public int UpdatedAt { get; set; }

      
        public ICollection<Product> products { get; set; } // Navigation prop
    }
}
