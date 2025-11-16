using Azure;
using E_commerceManagementSystem.BLL.Dto.CartDto;
using E_commerceManagementSystem.BLL.Dto.OrderDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.CartManager;
using E_commerceManagementSystem.BLL.Manager.OrderManager;
using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]

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
        public async Task<IActionResult> GetByIdAsync(int Id)
        {
            var result = await _cartManager.GetByIdAsync(Id);
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

        //[HttpPut("{Id}")]
        //public async Task<IActionResult> UpdateCartItems(int Id,List<UpdateCartItemsInCartDto> cartItemsDto)
        //{
        //    var result = await _cartManager.UpdateCartItemsInCart(Id, cartItemsDto);

        //    if (!result.Success)
        //    {
        //        return StatusCode(result.StatusCode, result);
        //    }
        //    return Ok(result);
        //}

    }
}