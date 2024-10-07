using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.CategoryDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.CategoryManger;
using E_commerceManagementSystem.BLL.Manager.ProductManager;
using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManger _categoryManger;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryManger categoryManger, IMapper mapper)
        {
            _categoryManger = categoryManger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GeneralRespons), StatusCodes.Status200OK)] // 200 OK
        [ProducesResponseType(StatusCodes.Status404NotFound)] // 404 Not Found
        public async Task<ActionResult<GeneralRespons>> GetAll()
        {
            var response = await _categoryManger.GetAllAsync();
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GeneralRespons), StatusCodes.Status200OK)] // 200 OK
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // 400 Bad Request
        [ProducesResponseType(StatusCodes.Status404NotFound)] // 404 Not Found
        public async Task<ActionResult<GeneralRespons>> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "ID should be greater than zero." });
            }
            var response = await _categoryManger.GetByIdAsync(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("by-name/{categoryName}")]
        [ProducesResponseType(typeof(GeneralRespons), StatusCodes.Status200OK)] // 200 OK
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // 400 Bad Request
        [ProducesResponseType(StatusCodes.Status404NotFound)] // 404 Not Found
        public async Task<ActionResult<GeneralRespons>> GetByCategoryName(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                return BadRequest(new { message = "Category name should not be null or empty." });
            }
            var response = await _categoryManger.GetByCategoryNameAsync(categoryName);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(GeneralRespons), StatusCodes.Status201Created)] // 201 Created
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // 400 Bad Request
        public async Task<ActionResult<GeneralRespons>> Create(AddCategoryDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _categoryManger.AddAsync(model);
            if (!response.Success)
            {
                return NotFound(response);
            }

            return CreatedAtAction(nameof(GetById), new { id = (response.Model as Product)?.Id }, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(GeneralRespons), StatusCodes.Status200OK)] // 200 OK
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // 400 Bad Request
        [ProducesResponseType(StatusCodes.Status404NotFound)] // 404 Not Found
        public async Task<ActionResult<GeneralRespons>> Update(int id, [FromBody] UpdateCategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _categoryManger.UpdateAsync(id, model);
            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // 204 No Content
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // 400 Bad Request
        [ProducesResponseType(StatusCodes.Status404NotFound)] // 404 Not Found
        public async Task<ActionResult<GeneralRespons>> Delete(int id)
        {
            var response = await _categoryManger.DeleteAsync(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return NoContent(); // Return 204 No Content
        }
    }
}
