using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.ShippingDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.ShippingManger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class ShippingController : ControllerBase
    {
        private readonly IShippingManager _shippingManager;
        private readonly IMapper _mapper;

        public ShippingController(IShippingManager shippingManager, IMapper mapper)
        {
            _shippingManager = shippingManager;
            _mapper = mapper;
        }

        private ActionResult<GeneralRespons> HandleResponse(GeneralRespons response)
        {
            if (!response.Success)
            {
                return StatusCode((int)response.StatusCode, response); // Return the appropriate status code and response
            }
            return Ok(response); // Return 200 OK if successful
        }

        [HttpGet]
        public async Task<ActionResult<GeneralRespons>> GetAllAsync()
        {
            var response = await _shippingManager.GetAllAsync();
            return HandleResponse(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GeneralRespons>> GetByIdAsync(int id)
        {
            var response = await _shippingManager.GetByIdAsync(id);
            return HandleResponse(response);
        }

        [HttpGet("order/{orderId:int}")]
        public async Task<ActionResult<GeneralRespons>> GetByOrderIdAsync(int orderId)
        {
            if (orderId <= 0)
            {
                return BadRequest(new GeneralRespons { Success = false, Message = "Order ID should be greater than zero." });
            }
            var response = await _shippingManager.GetByOrderIdAsync(orderId);
            return HandleResponse(response);
        }

        [HttpGet("state/{shippingState}")]
        public async Task<ActionResult<GeneralRespons>> GetByShippingStateAsync(string shippingState)
        {
            if (string.IsNullOrWhiteSpace(shippingState))
            {
                return BadRequest(new GeneralRespons { Success = false, Message = "Shipping state should not be empty." });
            }
            var response = await _shippingManager.GetByShippingStateAsync(shippingState);
            return HandleResponse(response);
        }

        [HttpGet("tracking/{trackingNumber:int}")]
        public async Task<ActionResult<GeneralRespons>> GetByTrackingNumberAsync(string trackingNumber)
        {
            if (trackingNumber ==null)
            {
                return BadRequest(new GeneralRespons { Success = false, Message = "Tracking number should be greater than zero." });
            }
            var response = await _shippingManager.GetByTrackingNumberAsync(trackingNumber);
            return HandleResponse(response);
        }

        [HttpGet("date/{shippingDate}")]
        public async Task<ActionResult<GeneralRespons>> GetByShippingDateAsync(DateTime shippingDate)
        {
            if (shippingDate == DateTime.MinValue)
            {
                return BadRequest(new GeneralRespons { Success = false, Message = "Invalid shipping date." });
            }
            var response = await _shippingManager.GetByShippingDateAsync(shippingDate);
            return HandleResponse(response);
        }

        //[HttpPost]
        //public async Task<ActionResult<GeneralRespons>> CreateAsync([FromBody] AddShippingDto model)
        //{


        //    var response = await _shippingManager.AddAsync(model);
        //    if (!response.Success)
        //    {
        //        return StatusCode((int)response.StatusCode, response);
        //    }

        //    return Ok(response);
        //}

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GeneralRespons>> UpdateAsync(int id, [FromBody] UpdateShippingDto model)
        {


            var response = await _shippingManager.UpdateAsync(id, model);
            return HandleResponse(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<GeneralRespons>> DeleteAsync(int id)
        {
            var response = await _shippingManager.DeleteAsync(id);
            return HandleResponse(response);
        }
    }
}
