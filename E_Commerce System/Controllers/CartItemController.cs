using E_commerceManagementSystem.BLL.Dto.CartDto;
using E_commerceManagementSystem.BLL.Dto.CartItemDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.CartItemManager;
using E_commerceManagementSystem.BLL.Manager.CartManager;
using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Authorize(Roles = "Admin,User")]
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
                return NotFound(result);
            }
            return Ok(result.Model);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdAsync(int Id)
        {
            var result = await _cartItemManager.GetByIdAsync(Id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result.Model);
        }

        [HttpGet("CartId/{cartId}")]
        public async Task<IActionResult> GetByCartAsync(int cartId)
        {
            var result = await _cartItemManager.GetByCartIdAsync(cartId);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result.Model);
        }



        [HttpPost]
        public async Task<ActionResult<GeneralRespons>> AddAsync(AddCartItemDto dto)
        {

           
            var cartItemExists = await _cartItemManager.ValidInput(dto.CartID, dto.ProductId);
            if (!cartItemExists.Success)
            {
                return StatusCode(cartItemExists.StatusCode, cartItemExists);

            }

            var response = await _cartItemManager.AddAsync(dto);
            if (!response.Success)
            {
                return StatusCode(response.StatusCode,response);
            }

            return Ok(response);
            //return CreatedAtAction(nameof(GetByIdAsync), new { id = (response.Model as Cart)?.Id }, response); // Return 201 Created
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult<GeneralRespons>> Update(int id, [FromBody] UpdateCartItemDto dto)
        {
            var response = await _cartItemManager.UpdateAsync(id, dto);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralRespons>> Delete(int id)
        {
            var resppnse = await _cartItemManager.DeleteAsync(id);
            if (!resppnse.Success)
            {
                return BadRequest(resppnse.Message);
            }
            return Ok(resppnse.Message);
        }
    }
}