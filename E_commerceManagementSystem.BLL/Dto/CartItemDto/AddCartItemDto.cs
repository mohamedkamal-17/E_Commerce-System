using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Dto.CartItemDto
{
    public class AddCartItemDto
    {
        [Required]
        public int Quantity { get; set; }
        [Required]

        public int ProductId { get; set; }
        [Required]

        public int CartID { get; set; }
    }
}
