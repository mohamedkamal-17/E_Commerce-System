using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.CartDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.CartItemRepository;
using E_commerceManagementSystem.DAL.Reposatories.CartRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_commerceManagementSystem.BLL.Manager.CartManager
{
    public class CartManager : Manager<Cart, ReadCartDto, AddCartDto, UpdateCartDto>, ICartManager
    {
        private readonly ICartRepo _repository;
        private readonly ICartItemRepo _cartItemRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartManager(ICartRepo repository, IMapper mapper, UserManager<ApplicationUser> userManager, ICartItemRepo cartItemRepo) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
            _cartItemRepo = cartItemRepo;
        }

        public override async Task<GeneralRespons> GetAllAsync()
        {

            var resultList = await _repository.GetAll(u => u.User)
                        .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.Product)
                        .ToListAsync();

            if (resultList != null && resultList.Count > 0)
            {


                var dtoList = _mapper.Map<List<ReadCartDto>>(resultList);
                return CreateResponse(true, dtoList, "Carts retrieved successfully.", 204);
            }
            if (resultList != null && resultList.Count == 0)
            {

                return CreateResponse(true, null, "Carts retrieved successfully,but no cart exest", 200);
            }

            return CreateResponse(false, null, "Carts not found.", 404);
        }

        public async override Task<GeneralRespons> GetByIdAsync(int id)
        {
            var idExsist = _repository.GetAll().Any(c => c.Id == id);
            if (idExsist)
            {
                var cart = await _repository.GetAll(c => c.Id == id, u => u.User)
                                                    .Include(c => c.CartItems)
                                                     .ThenInclude(ci => ci.Product)
                                                    .FirstOrDefaultAsync();

                return CreateResponse(true, _mapper.Map<ReadCartDto>(cart), $"{typeof(Cart).Name} retrieved successfully.", 200);
            }
            else
                return CreateResponse(false, null, $"no cart with this id.", 404);

        }


        public async Task<GeneralRespons> GetByUserIdAsync(string userId)
        {
            var userExists = await _userManager.FindByIdAsync(userId);
            if (userExists == null)
            {
                return CreateResponse(false, null, "No user with this id.", 404); // Not Found
            }

            var cart = await _repository.GetByConditionAsync(x => x.UserId == userId)
                .Include(c => c.CartItems)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync();

            if (cart == null)
            {
                return CreateResponse(false, null, "No cart found for the given name.", 404); // Not Found
            }
            var readDto = _mapper.Map<ReadCartDto>(cart);
            return CreateResponse(true, readDto, "cart retrieved successfully", 200); // OK

        }
        public async Task RemoveCartItems(IEnumerable<CartItem> cartItems)
        {
            await _repository.RemoveCartItemsAsync(cartItems);
            //save one time after all changes
            await _repository.SaveChangesAsync();
        }
    }
}
