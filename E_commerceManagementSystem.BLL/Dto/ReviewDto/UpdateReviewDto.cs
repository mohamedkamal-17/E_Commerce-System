using E_commerceManagementSystem.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Dto.ReviewDto
{
    public class UpdateReviewDto
    {
        public int Id { get; set; } 
        public int Rating { get; set; } 
        public string ReviewText { get; set; }
        public int ProductId { get; set; } 
        public string UserId { get; set; } 
    }
}
