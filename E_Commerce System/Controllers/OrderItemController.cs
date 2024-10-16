using E_commerceManagementSystem.BLL.Dto.OrederItemDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.OrderItemManager;
using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemManager _orderItemManager;

        public OrderItemController(IOrderItemManager orderItemManager)
        {
            _orderItemManager = orderItemManager;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralRespons>> GetAllAsync()
        {
            var result = await _orderItemManager.GetAllAsync();
            if (!result.Success)
            {
                // Check status code in response and return appropriate result
                return StatusCode(result.StatusCode, result);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralRespons>> GetByIdAsync(int id)
        {
            var result = await _orderItemManager.GetByIdAsync(id);
            if (!result.Success)
            {
                // Check status code in response and return appropriate result
                return StatusCode(result.StatusCode, result);
            }
            return Ok(result);
        }

        [HttpGet("OrderId/{orderId}")]
        public async Task<ActionResult<GeneralRespons>> GetByOrderIdAsync(int orderId)
        {
            var result = await _orderItemManager.GetByOrderIdAsync(orderId);
            if (!result.Success)
            {
                // Check status code in response and return appropriate result
                return StatusCode(result.StatusCode, result);
            }
            return Ok(result);
        }

        //[HttpPost]
        //public async Task<ActionResult<GeneralRespons>> AddAsync(AddOrderItemDto dto)
        //{
        //    var response = await _orderItemManager.AddAsync(dto);
        //    if (!response.Success)
        //    {
        //        // Check status code in response and return appropriate result
        //        return StatusCode(response.StatusCode, response);
        //    }

        //    return CreatedAtAction(nameof(GetByIdAsync), new { id = (response.Model as OrderItem)?.Id }, response); // Return 201 Created
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<GeneralRespons>> Update(int id, [FromBody] UpdateOrderItemDto dto)
        //{
        //    var response = await _orderItemManager.UpdateAsync(id, dto);
        //    if (!response.Success)
        //    {
        //        // Check status code in response and return appropriate result
        //        return StatusCode(response.StatusCode, response);
        //    }

        //    return Ok(response);
        //}

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralRespons>> Delete(int id)
        {
            var response = await _orderItemManager.DeleteAsync(id);
            if (!response.Success)
            {
                // Check status code in response and return appropriate result
                return StatusCode(response.StatusCode, response);
            }

            return Ok(response);
        }
    }
}
