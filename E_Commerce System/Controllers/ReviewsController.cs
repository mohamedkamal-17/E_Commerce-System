using E_commerceManagementSystem.BLL.Dto.ReviewDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.ReviewManager;
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
        public async Task<ActionResult<GeneralRespons>> GetAllAsync()
        {
            var result = await _reviewManager.GetAllAsync();
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Model);
        }

        [HttpGet("{id:int}")] // Route constraint to ensure id is an int
        public async Task<ActionResult<GeneralRespons>> GetByIdAsync(int id)
        {
            var result = await _reviewManager.GetByIdAsync(id);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Model);
        }

        [HttpGet("product/{productId:int}")] // Changed for clarity and to avoid conflicts
        public async Task<ActionResult<GeneralRespons>> GetByProductIdAsync(int productId)
        {
            var result = await _reviewManager.GetByProductIdAsync(productId);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Model);
        }

        [HttpGet("user/{userId}")] // Changed for clarity and to avoid conflicts
        public async Task<ActionResult<GeneralRespons>> GetByUserIdAsync(string userId)
        {
            var result = await _reviewManager.GetByUserIdAsync(userId);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Model);
        }

        [HttpPost]
        public async Task<ActionResult<GeneralRespons>> AddAsync([FromBody] AddReviewDto addReviewDto)
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
            return CreatedAtAction(nameof(GetByIdAsync), new { id = (result.Model as ReadReviewDto)?.Id }, result.Model); // Return 201 Created
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GeneralRespons>> UpdateAsync(int id, [FromBody] UpdateReviewDto updateReviewDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reviewExists = await _reviewManager.GetByIdAsync(id);
            if (!reviewExists.Success)
            {
                return NotFound(reviewExists.Message);
            }
            var result = await _reviewManager.UpdateAsync(id, updateReviewDto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Model);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<GeneralRespons>> DeleteAsync(int id)
        {
            var result = await _reviewManager.DeleteAsync(id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Model);
        }
    }
}
