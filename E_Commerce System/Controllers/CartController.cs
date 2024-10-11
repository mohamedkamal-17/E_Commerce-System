using E_commerceManagementSystem.BLL.Dto.CartDto;
using E_commerceManagementSystem.BLL.Dto.OrderDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.CartManager;
using E_commerceManagementSystem.BLL.Manager.OrderManager;
using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartManager _cartManager;
        public CartController(ICartManager cartManager)
        {
            _cartManager = cartManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _cartManager.GetAllWithIncludesAsync(u => u.User, c => c.CartItems);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Model);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _cartManager.GetByIdAsync(id);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Model);
        }

        [HttpGet("UserId/{Id}")]
        public async Task<IActionResult> GetByUserAsync(string userId)
        {
            var result = await _cartManager.GetByUserIdAsync(userId);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Model);
        }

        [HttpPost]
        public async Task<ActionResult<GeneralRespons>> AddAsync(AddCartDto dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _cartManager.AddAsync(dto);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return CreatedAtAction(nameof(GetByIdAsync), new { id = (response.Model as Cart)?.Id }, response); // Return 201 Created
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralRespons>> Update(int id, [FromBody] UpdateCartDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _cartManager.UpdateAsync(id, dto);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralRespons>> Delete(int id)
        {

            var resppnse = await _cartManager.DeleteAsync(id);
            if (!resppnse.Success)
            {
                return BadRequest(resppnse.Message);
            }
            return Ok(resppnse.Model);
        }


    }
}