using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Dto.WishListItemsDto
{
    public class AddWishlistItemsDto
    {
        public int WishListId { get; set; }  // Foreign Key referencing WishList
        public int ProductId { get; set; }
    }
}
