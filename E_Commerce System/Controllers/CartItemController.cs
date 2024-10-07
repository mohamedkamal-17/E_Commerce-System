using E_commerceManagementSystem.BLL.Dto.CartDto;
using E_commerceManagementSystem.BLL.Dto.CartItemDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.CartItemManager;
using E_commerceManagementSystem.BLL.Manager.CartManager;
using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemManager _cartItemManager;
        public CartItemController(ICartItemManager cartItemManager)
        {
            _cartItemManager = cartItemManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _cartItemManager.GetAllAsync();
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Model);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _cartItemManager.GetByIdAsync(id);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Model);
        }

        [HttpGet("CartId/{cartId}")]
        public async Task<IActionResult> GetByUserAsync(int cartId)
        {
            var result = await _cartItemManager.GetByCartIdAsync(cartId);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Model);
        }

        [HttpPost]
        public async Task<ActionResult<GeneralRespons>> AddAsync(AddCartItemDto dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _cartItemManager.AddAsync(dto);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return CreatedAtAction(nameof(GetByIdAsync), new { id = (response.Model as Cart)?.Id }, response); // Return 201 Created
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralRespons>> Update(int id, [FromBody] UpdateCartItemDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _cartItemManager.UpdateAsync(id, dto);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralRespons>> Delete(int id)
        {

            var resppnse = await _cartItemManager.DeleteAsync(id);
            if (!resppnse.Success)
            {
                return BadRequest(resppnse.Message);
            }
            return Ok(resppnse.Model);
        }
    }
}