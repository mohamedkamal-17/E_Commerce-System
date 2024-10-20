//using AutoMapper;
//using E_commerceManagementSystem.BLL.Dto.WishlistDto;
//using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
//using E_commerceManagementSystem.BLL.Manager.WishlistManager;
//using E_commerceManagementSystem.DAL.Data.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace E_Commerce_System.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize(Roles = "Admin,User")]
//    public class WishListController : ControllerBase
//    {
//        private readonly IWishlistManager _wishlistManager;
//        private readonly IMapper _mapper;

//        public WishListController(IWishlistManager wishlistManager, IMapper mapper)
//        {
//            _wishlistManager = wishlistManager;
//            _mapper = mapper;
//        }

//        private ActionResult<GeneralRespons> HandleResponse(GeneralRespons response)
//        {
//            if (!response.Success)
//            {
//                return StatusCode((int)response.StatusCode, response); // Return the appropriate status code and response
//            }
//            return Ok(response); // Return 200 OK if successful
//        }

//        [HttpGet]
//        public async Task<ActionResult<GeneralRespons>> GetAllAsync()
//        {
//            var response = await _wishlistManager.GetAllAsync();
//            return HandleResponse(response);
//        }

//        [HttpGet("id/{id:int}")]
//        public async Task<ActionResult<GeneralRespons>> GetByIdAsync(int id)
//        {
//            if (id <= 0)
//            {
//                return BadRequest(new GeneralRespons { Success = false, Message = "ID should be greater than zero." });
//            }
//            var response = await _wishlistManager.GetByIdAsync(id);
//            return HandleResponse(response);
//        }

//        [HttpGet("UserID/{userId}")]
//        public async Task<ActionResult<GeneralRespons>> GetByUserIDAsync(string userId)
//        {
//            if (string.IsNullOrEmpty(userId))
//            {
//                return BadRequest(new GeneralRespons { Success = false, Message = "User ID should not be empty." });
//            }
//            var response = await _wishlistManager.GetByUserID(userId);
//            return HandleResponse(response);
//        }

//        [HttpPost]
//        public async Task<ActionResult<GeneralRespons>> CreateAsync([FromBody] AddWishlistDto model)
//        {
          
//            var response = await _wishlistManager.AddAsync(model);
//            if (!response.Success)
//            {
//                return StatusCode((int)response.StatusCode, response);
//            }

//            return CreatedAtAction(nameof(GetByIdAsync), new { id = (response.Model as WishList)?.Id }, response);
//        }

//        [HttpPut("{id:int}")]
//        public async Task<ActionResult<GeneralRespons>> UpdateAsync(int id, [FromBody] UpdateWishlistDto model)
//        {
        

//            var response = await _wishlistManager.UpdateAsync(id, model);
//            return HandleResponse(response);
//        }

//        [HttpDelete("{id:int}")]
//        public async Task<ActionResult<GeneralRespons>> DeleteAsync(int id)
//        {
//            var response = await _wishlistManager.DeleteAsync(id);
//            return HandleResponse(response);
//        }
//    }
//}
