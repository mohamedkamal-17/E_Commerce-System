using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.WishlistDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.BLL.Manager.WishlistManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.WishListItemsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.WishlistItemsManager
{
    public interface IWishlistItemsManager : IManager<WishListItems, ReadWishlistDto, AddWishlistDto, UpdateWishlistDto>
    {
      
    }
}
