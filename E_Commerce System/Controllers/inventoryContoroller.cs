using E_commerceManagementSystem.BLL.Dto.CategoryDto;
using E_commerceManagementSystem.BLL.Dto.InventoryDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.InventoryManager;
using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class inventoryContoroller : ControllerBase
    {
        private readonly IInventoryManagre _Inventorymanger;

        public inventoryContoroller(IInventoryManagre inventorymanger)
        {
            _Inventorymanger = inventorymanger;
        }
        public async Task<ActionResult<GeneralRespons>> GetAll()
        {

            var response = await _Inventorymanger.GetAllAsync();
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);


        }



        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralRespons>> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { meassge = "id shoud largTha Zero" });
            }
            var response = await _Inventorymanger.GetByIdAsync(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpGet("{ProductId}")]
        public async Task<ActionResult<GeneralRespons>> GetByProductId(int productId)
        {
            if (productId <=0)
            {
                return BadRequest(new { meassge = "id shoud largTha Zero" });
            }
            var response = await _Inventorymanger.GetByProductId(productId);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<GeneralRespons>> Create([FromBody]AddInventoryDto model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _Inventorymanger.AddAsync(model);
            if (!response.Success)
            {
                return NotFound(response);
            }

            return CreatedAtAction(nameof(GetById), new { id = (response.Model as Product)?.Id }, response); // Return 201 Created


        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralRespons>> Update(int id, [FromBody] UpdateInventoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _Inventorymanger.UpdateAsync(id, model);
            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralRespons>> Delete(int id)
        {

            var resppnse = await _Inventorymanger.DeleteAsync(id);
            if (!resppnse.Success)
            {
                return BadRequest(resppnse);
            }
            return Ok(resppnse);
        }
    }
}
