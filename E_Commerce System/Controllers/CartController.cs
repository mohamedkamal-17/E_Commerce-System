using Azure;
using E_commerceManagementSystem.BLL.Dto.CartDto;
using E_commerceManagementSystem.BLL.Dto.OrderDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.CartManager;
using E_commerceManagementSystem.BLL.Manager.OrderManager;
using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
            var result = await _cartManager.GetAllAsync();
            if (!result.Success)
            {
                return StatusCode (result.StatusCode, result);
            }
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _cartManager.GetByIdAsync(id);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result);
            }
            return Ok(result);
        }

        [HttpGet("UserId/{userId}")]
        public async Task<IActionResult> GetByUserAsync(string userId)
        {
            var result = await _cartManager.GetByUserIdAsync(userId);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<GeneralRespons>> AddAsync(AddCartDto dto)
        {
            var cartExists = await _cartManager.GetByUserIdAsync(dto.UserId);
            if(cartExists.Success)
            {
                return BadRequest("this user has already cart");
            }
            var response = await _cartManager.AddAsync(dto);
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
            //return CreatedAtAction(nameof(GetByIdAsync), new { id = (response.Model as Cart)?.Id }, response); // Return 201 Created
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralRespons>> Update(int id, [FromBody] UpdateCartDto dto)
        {
            var response = await _cartManager.UpdateAsync(id, dto);
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralRespons>> Delete(int id)
        {
            var respnse = await _cartManager.DeleteAsync(id);
            if (!respnse.Success)
            {
                return StatusCode(respnse.StatusCode, respnse);
            }

            return Ok(respnse);
        }
    }
}