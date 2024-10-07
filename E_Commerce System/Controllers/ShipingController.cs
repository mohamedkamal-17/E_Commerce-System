using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.ShippingDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.ShippingManger;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private readonly IShippingManger _shippingManger;
        private readonly IMapper _mapper;

        public ShippingController(IShippingManger shippingManger, IMapper mapper)
        {
            _shippingManger = shippingManger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralRespons>> GetAllAsync()
        {
            var response = await _shippingManger.GetAllAsync();
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("{id:int}")] // Ensure id is an integer
        public async Task<ActionResult<GeneralRespons>> GetByIdAsync(int id)
        {
            var response = await _shippingManger.GetByIdAsync(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("order/{orderId:int}")] // Changed to unique route
        public async Task<ActionResult<GeneralRespons>> GetByOrderIdAsync(int orderId)
        {
            if (orderId <= 0) // Check for valid orderId
            {
                return BadRequest(new { Message = "orderId should be greater than zero." });
            }
            var response = await _shippingManger.GetByOrderIdAsync(orderId);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("state/{shippingState}")] // Changed to unique route
        public async Task<ActionResult<GeneralRespons>> GetByShippingStateAsync(string shippingState)
        {
            if (string.IsNullOrEmpty(shippingState))
            {
                return BadRequest(new { Message = "ShippingState should not be empty." });
            }
            var response = await _shippingManger.GetByShippingState(shippingState);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("tracking/{trackingNumber:int}")] // Changed to unique route
        public async Task<ActionResult<GeneralRespons>> GetByTrackingNumberAsync(int trackingNumber)
        {
            if (trackingNumber <= 0) // Check for valid trackingNumber
            {
                return BadRequest(new { Message = "TrackingNumber should be greater than zero." });
            }
            var response = await _shippingManger.GetByTrackingNumber(trackingNumber);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("date/{shippingDate}")] // Changed to unique route
        public async Task<ActionResult<GeneralRespons>> GetByShippingDateAsync(DateTime shippingDate)
        {
            if (shippingDate == DateTime.MinValue) // Check for valid shippingDate
            {
                return BadRequest(new { Message = "Invalid shipping date." });
            }
            var response = await _shippingManger.GetByShippingDate(shippingDate);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<GeneralRespons>> CreateAsync(AddShippingDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _shippingManger.AddAsync(model);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return CreatedAtAction(nameof(GetByIdAsync), new { id = (response.Model as ReadShippingDto)?.Id }, response); // Return 201 Created
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GeneralRespons>> UpdateAsync(int id, [FromBody] UpdateShippingDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _shippingManger.UpdateAsync(id, model);
            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<GeneralRespons>> DeleteAsync(int id)
        {
            var response = await _shippingManger.DeleteAsync(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
