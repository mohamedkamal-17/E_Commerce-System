using E_commerceManagementSystem.BLL.Dto.CartItemDto;
using E_commerceManagementSystem.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Dto.CartDto
{
    public class ReadCartDto
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UserId { get; set; } 
        public string UserName { get; set; }
        public ICollection<ReadCartItemDto>? readCartItemDtos { get; set; }
            
    }
}
