using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.OrderDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.BLL.Manager.InventoryManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.CartRepository;
using E_commerceManagementSystem.DAL.Reposatories.InventoryRepository;
using E_commerceManagementSystem.DAL.Reposatories.OrederRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_commerceManagementSystem.BLL.Manager.OrderManager
{
    public class OrderManager : Manager<Order, ReadOrderDto, AddOrderDto, UpdateOrderDto>, IOrderManager
    {
        private readonly IOrderRepo _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ICartRepo _cartRepo;
        private readonly IInventoryManager _inventoryManager;
        private readonly IInventoryRepo _inventoryRepo;

        public OrderManager(IOrderRepo repository, IMapper mapper, UserManager<ApplicationUser> userManager, ICartRepo cartRepo, IInventoryManager inventoryManager, IInventoryRepo inventoryRepo)
            : base(repository, mapper)
        {
            _repository = repository;
            _userManager = userManager;
            _mapper = mapper;
            _cartRepo = cartRepo;
            _inventoryManager = inventoryManager;
            _inventoryRepo = inventoryRepo;
        }
        public override async Task<GeneralRespons> GetAllAsync()
        {
            return await base.GetAll(o => o.OrderItems, o => o.User);
        }
        public override Task<GeneralRespons> GetByIdAsync(int id)
        {
            return base.GetAllByConditionAndIncludes(o => o.Id == id, o => o.OrderItems, o => o.User);
        }
        public async Task<GeneralRespons> GetByUserIdAsync(string userId)
        {

            return await base.GetAllByConditionAndIncludes(o => o.UserId == userId, o => o.OrderItems, o => o.User);

        }

        public override async Task<GeneralRespons> AddAsync(AddOrderDto addDto)
        {
            var cart = await _cartRepo.GetAll(c => c.Id == addDto.CartId, u => u.User)
                                                 .Include(c => c.CartItems)
                                                 .ThenInclude(ci => ci.Product)
                                                 .FirstOrDefaultAsync();



            if (cart == null || cart.CartItems == null || !cart.CartItems.Any())
            {
                return CreateResponse(false, null, "Cart is empty or does not exist", 400);
            }

            //to get all products in carts and get this stockQuantity ine time to use it in two loop

            //get all products id in cartItem 
            var productIds = cart.CartItems.Select(ci => ci.ProductId).ToList();

            //get all products that matchs with product id in list of [productIds]
            var inventoryList = await _inventoryRepo.GetAll(i => productIds.Contains(i.ProductId)).ToListAsync();

            //create a dictionary for fast look up by ProductId
            var inventoryDict = inventoryList.ToDictionary(i => i.ProductId);

            foreach (var cartItem in cart.CartItems)
            {

                //------this is old version that i check for productids from database two time in this loop and in the 2nd loop
                //var inventory = await _inventoryRepo.GetAll(i => i.ProductId == cartItem.ProductId)
                //    .FirstOrDefaultAsync();
                //if (inventory == null)
                //{
                //    return CreateResponse(false, null, $"Inventory for product {cartItem.Product.ProductName} not found.", 404);
                //}

                //-------------new version check if the product exists in the inventory dictionary
                if (!inventoryDict.TryGetValue(cartItem.ProductId, out var inventory))
                {
                    return CreateResponse(false, null, $"Inventory for product {cartItem.Product.ProductName} not found.", 404);
                }

                if (cartItem.Quantity > inventory.StockQuantity)
                {
                    return CreateResponse(false, null, $"Product {cartItem.Product.ProductName} has insufficient stock. Available: {inventory.StockQuantity}, Requested: {cartItem.Quantity}", 400);
                }
            }


            double totalPrice = cart.CartItems.Sum(item => item.Product.Price * item.Quantity);

            var order = new Order
            {
                UserId = cart.UserId,
                Address = addDto.Address,
                TotalPrice = totalPrice,
                PaymentId = addDto.PaymentId,
                OrderItems = _mapper.Map<List<OrderItem>>(cart.CartItems)
            };

            await _repository.AddAsync(order);

            foreach (var cartItem in cart.CartItems)
            {
                if (inventoryDict.TryGetValue(cartItem.ProductId, out var inventory))
                {
                    inventory.StockQuantity -= cartItem.Quantity;
                    await _inventoryRepo.UpdateAsync(inventory);
                }
            }
            var readOrderDto = _mapper.Map<ReadOrderDto>(order);

            await _cartRepo.RemoveCartItemsAsync(cart.CartItems);

            //save one time after all changes
            await _repository.SaveChangesAsync();
            return CreateResponse(true, readOrderDto, "Order added successfully", 201);

        }
    }
}