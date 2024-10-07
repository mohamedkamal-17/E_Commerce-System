using E_commerceManagementSystem.BLL.Dto.ReviewDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.ReviewManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewManager _reviewManager;

        public ReviewsController(IReviewManager reviewManager)
        {
            _reviewManager = reviewManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _reviewManager.GetAllAsync();
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Model);
        }

        [HttpGet("Id{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _reviewManager.GetByIdAsync(id);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Model);
        }

        [HttpGet("ProductId{productId}")]
        public async Task<IActionResult> GetByProductIdAsync(int productId)
        {
            var result = await _reviewManager.GetByProductIdAsync(productId);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Model);
        }

        [HttpGet("UserId{userId}")]
        public async Task<IActionResult> GetByUserIdAsync(string userId)
        {
            var result = await _reviewManager.GetByUserIdAsync(userId);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Model);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddReviewDto addReviewDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _reviewManager.AddAsync(addReviewDto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Model);
        }

        [HttpPut("{id}")] 
        public async Task<IActionResult> UpdateAsync(int id,UpdateReviewDto updateReviewDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reviewExists = await _reviewManager.GetByIdAsync(id);
            if(!reviewExists.Success)
            {
                return NotFound(reviewExists.Message);
            }
            var result = await _reviewManager.UpdateAsync(id,updateReviewDto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _reviewManager.DeleteAsync(id);
            if(!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Model);
        }
    }
}
