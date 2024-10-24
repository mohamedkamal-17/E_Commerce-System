using E_commerceManagementSystem.BLL.Dto.OrederItemDto;
using E_commerceManagementSystem.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Dto.OrderDto
{
    public class AddOrderDto
    {
        [Required]
        public int CartId { get; set; }
        [Required]

        public string Address { get; set; }
        [Required]

        public int PaymentId { get; set; }

    }
}
