using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_commerceManagementSystem.BLL.Dto.WishlistDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.WishlistRepsitory;
using Microsoft.EntityFrameworkCore;
using System.Net; // Added for HTTP status codes

namespace E_commerceManagementSystem.BLL.Manager.WishlistManager
{
    public class WishlistManager : Manager<WishList, ReadWishlistDto, AddWishlistDto, UpdateWishlistDto>, IWishlistManager
    {
        private readonly IWishlistRepo _wishlistRepo;
        private readonly IMapper _mapper;

        public WishlistManager(IWishlistRepo wishlistRepo, IMapper mapper)
            : base(wishlistRepo, mapper)
        {
            _wishlistRepo = wishlistRepo;
            _mapper = mapper;
        }

        public async Task<GeneralRespons> GetByUserID(string userId)
        {
            return await base.GetAllByConditionAndIncludes(wish => wish.UserId == userId, wish => wish.User);

            // Fetch the wish lists for the specified userId
            var wishLists = await _wishlistRepo.GetByConditionAsync(wish => wish.UserId == userId)
                .ProjectTo<ReadWishlistDto>(_mapper.ConfigurationProvider) // Assuming you have AutoMapper configured
                .ToListAsync(); // Use ToListAsync to execute the query asynchronously

            // Check if the wishLists is null or empty
            if (wishLists == null || wishLists.Count() == 0)
            {
                return CreateResponse(false, null, "No wish lists found for the specified user.",
                    (int)HttpStatusCode.NotFound); // 404 Not Found
            }

            return CreateResponse(true, wishLists, "Wish lists retrieved successfully.",
                (int)HttpStatusCode.OK); // 200 OK

        }
    }
}
