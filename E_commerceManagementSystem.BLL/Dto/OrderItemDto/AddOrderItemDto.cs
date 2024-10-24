using E_commerceManagementSystem.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Dto.OrederItemDto
{
    public class AddOrderItemDto
    {
        [Required]

        public int ProductId { get; set; }
        [Required]

        public int Quantity { get; set; }
    }
}
