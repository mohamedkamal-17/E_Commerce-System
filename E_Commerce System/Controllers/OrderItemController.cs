using E_commerceManagementSystem.BLL.Dto.OrderDto;
using E_commerceManagementSystem.BLL.Dto.OrederItemDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.OrderItemManager;
using E_commerceManagementSystem.BLL.Manager.OrderManager;
using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemManager _orderItemManager;
        public OrderItemController(IOrderItemManager orderItemManager)
        {
            _orderItemManager = orderItemManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _orderItemManager.GetAllAsync();
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Model);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _orderItemManager.GetByIdAsync(id);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Model);
        }

        [HttpGet("OrderId/{Id}")]
        public async Task<IActionResult> GetByUserAsync(int orderId)
        {
            var result = await _orderItemManager.GetByOrderIdAsync(orderId);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Model);
        }

        [HttpPost]
        public async Task<ActionResult<GeneralRespons>> AddAsync(AddOrderItemDto dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _orderItemManager.AddAsync(dto);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return CreatedAtAction(nameof(GetByIdAsync), new { id = (response.Model as OrderItem)?.Id }, response); // Return 201 Created
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralRespons>> Update(int id, [FromBody] UpdateOrderItemDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _orderItemManager.UpdateAsync(id, dto);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralRespons>> Delete(int id)
        {

            var resppnse = await _orderItemManager.DeleteAsync(id);
            if (!resppnse.Success)
            {
                return BadRequest(resppnse.Message);
            }
            return Ok(resppnse.Model);
        }


    }
}
