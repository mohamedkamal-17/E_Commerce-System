using E_commerceManagementSystem.BLL.Dto.OrederItemDto;
using E_commerceManagementSystem.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Dto.OrderDto
{
    public class AddOrderDto
    {
        public string UserId { get; set; }
        public string Address { get; set; }
        public int PaymentId { get; set; }  
        public ICollection<AddOrderItemDto>? addOrderItemDtos { get; set; }
    }
}
