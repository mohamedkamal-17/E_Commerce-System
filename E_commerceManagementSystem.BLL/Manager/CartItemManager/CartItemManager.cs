using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_commerceManagementSystem.BLL.Dto.CartItemDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.CartItemRepository;
using E_commerceManagementSystem.DAL.Reposatories.CartRepository;
using E_commerceManagementSystem.DAL.Reposatories.ProductRepository;
using Microsoft.EntityFrameworkCore;

namespace E_commerceManagementSystem.BLL.Manager.CartItemManager
{
    public class CartItemManager : Manager<CartItem, ReadCartItemDto, AddCartItemDto, UpdateCartItemDto>, ICartItemManager
    {
        private readonly ICartItemRepo _cartItemrepository;
        private readonly IMapper _mapper;
        private readonly ICartRepo _cartRepo;
        private readonly IProductRepo _productRepo;
        public CartItemManager(ICartItemRepo repository, IMapper mapper, ICartRepo cartRepo, IProductRepo productRepo) : base(repository, mapper)
        {
            _cartItemrepository = repository;
            _mapper = mapper;
            _cartRepo = cartRepo;
            _productRepo = productRepo;
        }
        public async override Task<GeneralRespons> GetAllAsync()
        {
            // Include the navigation properties you need
            return await base.GetAll(p => p.Product);
        }


        public async override Task<GeneralRespons> GetByIdAsync(int id)
        {
            var result = await _cartItemrepository.GetAll(e => e.Id == id, p => p.Product)
                      .AsNoTracking()
                      .ProjectTo<ReadCartItemDto>(_mapper.ConfigurationProvider)
                      .FirstOrDefaultAsync();

            if (result != null)
                return CreateResponse(true, result, $"{typeof(CartItem).Name}s retrieved successfully.", 200);

            return CreateResponse(false, null, $"{typeof(CartItem).Name} not found.", 404);
        }

        public async Task<GeneralRespons> GetByCartIdAsync(int cartId)
        {
            var cartExists = _cartRepo.GetAll().Any(c => c.Id == cartId);
            if (!cartExists)
            {
                return CreateResponse(false, null, "No cart with this id.", 404); // Not Found
            }
            return await base.GetAllByConditionAndIncludes(c => c.CartID == cartId, p => p.Product);
        }

        public async Task<bool> CheckCartExistsAsync(int cartId)
        {
            return await _cartRepo.GetAll().AnyAsync(c => c.Id == cartId);
        }

        public async Task<bool> CheckProductExistsAsync(int productId)
        {
            return await _productRepo.GetAll().AnyAsync(p => p.Id == productId);
        }

        public async Task<GeneralRespons> ValidInput(int cartId, int productId)
        {

            var cartExists = await CheckCartExistsAsync(cartId);
            if (!cartExists)
            {
                return CreateResponse(false, null, "No cart with this id.", 404); // Not Found
            }

            var productExists = await CheckProductExistsAsync(productId);
            if (!productExists)
            {
                return CreateResponse(false, null, "No product with this id.", 404);
            }

            var result = await base.GetAllByConditionAndIncludes(c => c.CartID == cartId && c.Product.Id == productId, p => p.Product);

            if (result.Success)
            {
                return CreateResponse(false, null, "this Product Exist in this cart.", 400);

            }
            return CreateResponse(true, result.Model, "valid dto", 200);

        }
    }
}
