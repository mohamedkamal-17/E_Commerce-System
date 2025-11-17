using Azure;
using E_commerceManagementSystem.BLL.Dto.OrderDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.OrderManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentService;
using PaymentService.DTOs;
using Stripe;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager _orderManager;
        private readonly PaymentGrpc.PaymentGrpcClient _grpcClient;

        public OrderController(IOrderManager orderManager, PaymentGrpc.PaymentGrpcClient grpcClient)
        {
            _orderManager = orderManager;
            _grpcClient = grpcClient;
        }
        [AllowAnonymous]
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

        [HttpPost("orders/{orderId}/pay")]

        public async Task<ActionResult> PayOrder(int orderId)
        {
            var response = await _orderManager.GetByIdAsync(orderId);
            if (!response.Success)
                return StatusCode(response.StatusCode, response);

            var order = response.Model as ReadOrderDto;

            if (order == null)
                return BadRequest("Order data is missing");

            var grpcRequest = new CreatePaymentRequest
            {
                OrderId = orderId,
                UserId = order.UserId,
                Amount = (double)order.TotalPrice,
                CustomerEmail = order.UserEmail,
                CustomerFirstName = order.UserName,
                CustomerLastName = "",
                CustomerPhone = order.UserPhoneNumber,
                Currency = "EGP",
                PaymentMethod = "card",
                Gateway = "paymob"
            };

            var grpcResponse = await _grpcClient.CreatePaymentAsync(grpcRequest);
            if (grpcResponse.Status != "success")
                return BadRequest(grpcResponse.ErrorMessage);

            return Ok(new
            {
                RedirectUri = grpcResponse.RedirectUrl,
                PaymentId = grpcResponse.PaymentId,
                paymentToken = grpcResponse.PaymentToken
            });
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
