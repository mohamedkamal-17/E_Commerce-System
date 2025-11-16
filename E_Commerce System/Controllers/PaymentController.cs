//using E_commerceManagementSystem.BLL.Dto.PaymentDto;
//using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
//using E_commerceManagementSystem.BLL.Manager.PaymentManager;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace E_Commerce_System.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize(Roles = "Admin,User")]
//    public class PaymentController : ControllerBase
//    {
//        private readonly IPaymentManger _paymentManager;

//        public PaymentController(IPaymentManger paymentManager)
//        {
//            _paymentManager = paymentManager;
//        }

//        // POST: api/Payment/process
//        [HttpPost("process")]
//        public async Task<IActionResult> ProcessPayment([FromBody] AddPaymentDto dto)
//        {
//            var response = await _paymentManager.ProcessPaymentAsync(dto);

//            if (response.Success)
//            {
//                return Ok(response);  // Return 200 with response
//            }
//            else
//            {
//                return StatusCode(response.StatusCode, response); // Return appropriate error code
//            }
//        }

//        // POST: api/Payment/confirm/{paymentIntentId}
//        [HttpPost("confirm/{paymentIntentId}")]
//        public async Task<IActionResult> ConfirmPayment(string paymentIntentId)
//        {
//            if (string.IsNullOrEmpty(paymentIntentId))
//            {
//                return BadRequest("Payment Intent ID is required.");
//            }

//            var response = await _paymentManager.ConfirmPaymentAsync(paymentIntentId);

//            if (response.Success)
//            {
//                return Ok(response);  // Return 200 with response
//            }
//            else
//            {
//                return StatusCode(response.StatusCode, response); // Return appropriate error code
//            }
//        }

//        // GET: api/Payment/{paymentId}
//        [HttpGet("{paymentId}")]
//        public async Task<IActionResult> GetPaymentDetails(int paymentId)
//        {
//            var response = await _paymentManager.GetByIdAsync(paymentId);

//            if (!response.Success)
//            {
//                // Check status code in response and return appropriate result
//                return StatusCode(response.StatusCode, response);
//            }

//            return Ok(response); // Return 200 with payment details
//        }
//    }
//}
