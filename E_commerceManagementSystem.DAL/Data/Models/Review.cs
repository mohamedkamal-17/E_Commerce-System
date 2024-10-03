using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.DAL.Data.Models
{
    public class Review
    {
        public int Id { get; set; } // (Primary Key) 
        public int Rating { get; set; } // (1-5) Scale
        public int ReviewText { get; set; }
        public int CreatedAt { get; set; }

        public int ProductId { get; set; } //Foreign Key referencing Product
        public Product Product { get; set; } //Navigation prop
        public string UserId { get; set; } //(Foreign Key referencing ApplicationUser) 
        public ApplicationUser User { get; set; } //Navigation prop


    }
}
