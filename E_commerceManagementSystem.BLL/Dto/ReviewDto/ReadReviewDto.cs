using E_commerceManagementSystem.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Dto.ReviewDto
{
    public class ReadReviewDto
    {
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ProductName { get; set; }
        public string UserName { get; set; } 

    }
}
