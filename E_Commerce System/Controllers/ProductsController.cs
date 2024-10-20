using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.ProductDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.ProductManager;
using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductMangare _productManager; // Corrected naming
        private readonly IMapper _mapper;

        public ProductsController(IProductMangare productManager, IMapper mapper)
        {
            _productManager = productManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralRespons>> GetAll()
        {
            var response = await _productManager.GetAllProducts();
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response); // Return appropriate error code
            }
            return Ok(response);
        }

        [HttpGet("{id:int}")] // Route constraint to ensure id is an int
        public async Task<ActionResult<GeneralRespons>> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "ID should be greater than zero." });
            }

            var response = await _productManager.GetByIdAsync(id);
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response); // Return appropriate error code
            }
            return Ok(response);
        }

        [HttpGet("name/{productName}")] // Changed to avoid conflict
        public async Task<ActionResult<GeneralRespons>> GetByProductName(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                return BadRequest(new { message = "Product name must be provided." });
            }

            var response = await _productManager.GetByProductNameAsync(productName);
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response); // Return appropriate error code
            }
            return Ok(response);
        }

        [HttpGet("category/{categoryName}")] // Changed to avoid conflict
        public async Task<ActionResult<GeneralRespons>> GetByCategoryName(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                return BadRequest(new { message = "Category name must be provided." });
            }

            var response = await _productManager.GetByCategoryNameAsync(categoryName);
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response); // Return appropriate error code
            }
            return Ok(response);
        }

        [HttpGet("price/{price:float}")] // Route constraint for float
        public async Task<ActionResult<GeneralRespons>> GetByPriceAsync(float price)
        {
            if (price < 0)
            {
                return BadRequest(new { message = "Price should be greater than or equal to zero." });
            }

            var response = await _productManager.GetByPriceAsync(price);
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response); // Return appropriate error code
            }
            return Ok(response);
        }

        [HttpGet("price/range/{highPrice:float}/{lowPrice:float}")] // More specific route
        public async Task<ActionResult<GeneralRespons>> GetByPriceInRangeAsync(float highPrice, float lowPrice)
        {
            if (highPrice < 0 || lowPrice < 0)
            {
                return BadRequest(new { message = "High price and low price must be non-negative." });
            }

            var response = await _productManager.GetByPriceInRangeAsync(highPrice, lowPrice);
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response); // Return appropriate error code
            }
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralRespons>> Add([FromBody] AddProductDto model)
        {
            var response = await _productManager.AddAsync(model);
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response); // Return appropriate error code
            }

            return Ok(new { response.Message, response.Model });
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralRespons>> Update(int id, [FromBody] UpdateProductDto model)
        {
            var response = await _productManager.UpdateAsync(id, model);
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response); // Return appropriate error code
            }

            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralRespons>> Delete(int id)
        {
            var response = await _productManager.DeleteAsync(id);
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response); // Return appropriate error code
            }
            return Ok(response);
        }
    }
}
