using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.ProductDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.InventoryRepository;
using E_commerceManagementSystem.DAL.Reposatories.ProductRepository;

namespace E_commerceManagementSystem.BLL.Manager.ProductManager
{
    public class ProductManager : Manager<Product, ReadProductDto, AddProductDto, UpdateProductDto>, IProductMangare
    {
        private readonly IProductRepo _repository;
        private readonly IMapper _mapper;
        private readonly IInventoryRepo _inventoryRepository;

        public ProductManager(IProductRepo repository, IMapper mapper, IInventoryRepo inventoryRepo)
            : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _inventoryRepository = inventoryRepo;
        }

        public async Task<GeneralRespons> GetAllProducts()
        {
            return await base.GetAll(p => p.Category);

        }
        public async Task<GeneralRespons> GetProductbyId(int id)
        {
            return await base.GetAll(p => p.Id == id);

        }
        public async Task<GeneralRespons> GetByPriceAsync(float price)
        {
            return await base.GetAllByConditionAndIncludes(p => p.Price == price, p => p.Category);

        }

        public async Task<GeneralRespons> GetByPriceInRangeAsync(float highPrice, float lowPrice)
        {
            return await base.GetAllByConditionAndIncludes(p => p.Price >= lowPrice && p.Price <= highPrice, p => p.Category);

        }

        public async Task<GeneralRespons> GetByPriceGreaterThanAsync(float lowPrice)
        {
            return await base.GetAllByConditionAndIncludes(p => p.Price > lowPrice, p => p.Category);

        }

        public async Task<GeneralRespons> GetByPriceLessThanAsync(float highPrice)
        {
            return await base.GetAll(p => p.Price < highPrice, p => p.Category);

        }
        public async Task<GeneralRespons> GetByCategoryNameAsync(string categoryName)
        {
            return await base.GetAllByConditionAndIncludes(p => p.Category.Name == categoryName, p => p.Category);

        }

        public async Task<GeneralRespons> GetByProductNameAsync(string productName)
        {
            return await base.GetAllByConditionAndIncludes(
                p => p.ProductName == productName, p => p.Category);
        }


        public override async Task<GeneralRespons> AddAsync(AddProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();

            var inventory = new Inventory
            {
                ProductId = product.Id,
                StockQuantity = product.StockQuantity ?? 0,
                ReorderLevel = 10,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _inventoryRepository.AddAsync(inventory);

            //save one time after all changes (add product and add inventory)
            await _repository.SaveChangesAsync();

            var ReadProduct = _mapper.Map<ReadProductDto>(product);
            return new GeneralRespons { Model = ReadProduct, Success = true, Message = "Product created successfully." };
        }
    }
}
