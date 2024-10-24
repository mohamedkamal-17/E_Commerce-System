using E_commerceManagementSystem.BLL.Dto.InventoryDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.InventoryManager;
using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryManager _inventoryManager;

        public InventoryController(IInventoryManager inventoryManager)
        {
            _inventoryManager = inventoryManager;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralRespons>> GetAll()
        {
            var response = await _inventoryManager.GetAllAsync();
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        [HttpGet("{id:int}")] // Route constraint to enforce int type
        public async Task<ActionResult<GeneralRespons>> GetById(int id)
        {
            if (id <= 0)
            {
              
                return BadRequest(new GeneralRespons
                {
                    Success = false,
                    Message = "ID should be greater than zero",
                    StatusCode = 400
                });
            }

            var response = await _inventoryManager.GetByIdAsync(id);
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        [HttpGet("product/{productId:int}")] // More descriptive route to avoid conflicts
        public async Task<ActionResult<GeneralRespons>> GetByProductId(int productId)
        {
            if (productId <= 0)
            {
               
                return BadRequest(new GeneralRespons
                {
                    Success = false,
                    Message = "Product ID should be greater than zero",
                    StatusCode = 400
                });
            }

            var response = await _inventoryManager.GetByProductId(productId);
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<GeneralRespons>> Create([FromBody] AddInventoryDto model)
        { 

            var response = await _inventoryManager.AddAsync(model);
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GeneralRespons>> Update(int id, [FromBody] UpdateInventoryDto model)
        {
          

            var response = await _inventoryManager.UpdateAsync(id, model);
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<GeneralRespons>> Delete(int id)
        {
            var response = await _inventoryManager.DeleteAsync(id);
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }
    }
}
