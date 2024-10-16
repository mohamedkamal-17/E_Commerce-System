using E_commerceManagementSystem.BLL.Dto.ReviewDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.ReviewManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewManager _reviewManager;

        public ReviewsController(IReviewManager reviewManager)
        {
            _reviewManager = reviewManager;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralRespons>> GetAllAsync()
        {
            var result = await _reviewManager.GetAllAsync();
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result); // Return appropriate error code
            }
            return Ok(result);
        }

        [HttpGet("{id:int}")] // Route constraint to ensure id is an int
        public async Task<ActionResult<GeneralRespons>> GetByIdAsync(int id)
        {
            var result = await _reviewManager.GetByIdAsync(id);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result); // Return appropriate error code
            }
            return Ok(result);
        }

        [HttpGet("product/{productId:int}")] // Changed for clarity and to avoid conflicts
        public async Task<ActionResult<GeneralRespons>> GetByProductIdAsync(int productId)
        {
            var result = await _reviewManager.GetByProductIdAsync(productId);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result); // Return appropriate error code
            }
            return Ok(result);
        }

        [HttpGet("user/{userId}")] // Changed for clarity and to avoid conflicts
        public async Task<ActionResult<GeneralRespons>> GetByUserIdAsync(string userId)
        {
            var result = await _reviewManager.GetByUserIdAsync(userId);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result); // Return appropriate error code
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<GeneralRespons>> AddAsync([FromBody] AddReviewDto addReviewDto)
        {
          
            var result = await _reviewManager.AddAsync(addReviewDto);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result); // Return appropriate error code
            }

            return Ok(result.Message); 
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GeneralRespons>> UpdateAsync(int id, [FromBody] UpdateReviewDto updateReviewDto)
        {
            var reviewExists = await _reviewManager.GetByIdAsync(id);
            if (!reviewExists.Success)
            {
                return StatusCode(reviewExists.StatusCode, reviewExists); // Return appropriate error code
            }

            var result = await _reviewManager.UpdateAsync(id, updateReviewDto);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result); // Return appropriate error code
            }

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<GeneralRespons>> DeleteAsync(int id)
        {
            var result = await _reviewManager.DeleteAsync(id);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result); // Return appropriate error code
            }
            return Ok(result);
        }
    }
}
