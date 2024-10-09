using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.WishlistDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.BLL.Manager.WishlistManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.WishListItemsRepository;
using E_commerceManagementSystem.DAL.Reposatories.WishlistRepsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.WishlistItemsManager
{
    public class WishlistItemsManager : Manager<WishListItems, ReadWishlistDto, AddWishlistDto, UpdateWishlistDto>, IWishlistItemsManager
    {
        private readonly IWishListItemsRepo _wishListItemsReo;
        private readonly IMapper _mapper;

        public WishlistItemsManager(IWishListItemsRepo wishListItemsReo, IMapper mapper) : base(wishListItemsReo, mapper)
        {
            _wishListItemsReo = wishListItemsReo;
            _mapper = mapper;
        }
    }

}
