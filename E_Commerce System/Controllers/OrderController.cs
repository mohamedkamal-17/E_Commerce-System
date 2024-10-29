using E_commerceManagementSystem.BLL.Dto.OrderDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.OrderManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager _orderManager;

        public OrderController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralRespons>> GetAllAsync()
        {
            var response = await _orderManager.GetAllAsync();
            if (!response.Success)
            {
                // Check status code in response and return appropriate result
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralRespons>> GetByIdAsync(int id)
        {
            var response = await _orderManager.GetByIdAsync(id);
            if (!response.Success)
            {
                // Check status code in response and return appropriate result
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        [HttpGet("UserId/{userId}")]
        public async Task<ActionResult<GeneralRespons>> GetByUserAsync(string userId)
        {
            var response = await _orderManager.GetByUserIdAsync(userId);
            if (!response.Success)
            {
                // Check status code in response and return appropriate result
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<GeneralRespons>> AddAsync([FromBody] AddOrderDto dto)
        {


            var response = await _orderManager.AddAsync(dto);
            if (!response.Success)
            {
                // Check status code in response and return appropriate result
                return StatusCode(response.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<GeneralRespons>> Update(int id, [FromBody] UpdateOrderDto dto)
        {


            var response = await _orderManager.UpdateAsync(id, dto);
            if (!response.Success)
            {
                // Check status code in response and return appropriate result
                return StatusCode(response.StatusCode, response);
            }

            return Ok(response.Message);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralRespons>> Delete(int id)
        {
            var response = await _orderManager.DeleteAsync(id);
            if (!response.Success)
            {
                // Check status code in response and return appropriate result
                return StatusCode(response.StatusCode, response);
            }

            return Ok(response.Message);
        }
    }
}
