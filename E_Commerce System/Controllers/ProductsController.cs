using AutoMapper;
using Azure;
using E_commerceManagementSystem.BLL.Dto.ProductDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.ProductManager;
using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductMangare _productMangare;
        private readonly IMapper _mapper;

        ProductsController(IProductMangare productMangare, IMapper mapper)
        {
            _productMangare = productMangare;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<ActionResult<GeneralRespons>> GetAll()
        {

            var response = await _productMangare.GetAllAsync();
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);


        }



        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralRespons>> GetById(int id)
        {
            if(id <= 0)
            {
                return BadRequest(new { meassge = "id shoud larg Than Zero" });
            }
            var response = await _productMangare.GetByIdAsync(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        //Task<GeneralRespons> GetByCategoryNameAsync(string categoryName);
        [HttpGet("{PrpductName")]
        public async Task<ActionResult<GeneralRespons>> GetByProductName(string PrpductName)
        {
            if (PrpductName == null)
            {
                return BadRequest(new { message = "shoud Enter PrpductName" });
            }
            var response = await _productMangare.GetByProductNameAsync(PrpductName);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpGet("{CategoryName}")]
        public async Task<ActionResult<GeneralRespons>> GetByCategoryName(string CategoryName)
        {
            if (CategoryName == null)
            {
                return BadRequest(new { message = "shoud Enter CategoryName" });
            }
            var response = await _productMangare.GetByCategoryNameAsync(CategoryName);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);

        }
        //Task<GeneralRespons> GetByPraiceAsync(float price);
        [HttpGet("{price}")]
        public async Task<ActionResult<GeneralRespons>> GetByPraiceAsync(float price)
        {
            if (price < 0)
            {
                return BadRequest(new { message = "shoud Enter price" });
            }
            var response = await _productMangare.GetByPraiceAsync(price);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);

        }

        //Task<GeneralRespons> GetByPraiceInRangeAsync(float highPrice, float lowPrice);
        [HttpGet("{highPrice}/{lowPrice}")]
        public async Task<ActionResult<GeneralRespons>> GetByPraiceInRangeAsync(float highPrice, float lowPrice)
        {

            if (highPrice < 0 || lowPrice < 0)
            {
                return BadRequest(new{ message= "highPrice or lowPrice not Exit "});
            }
            var response = await _productMangare.GetByPraiceInRangeAsync(highPrice,lowPrice);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);

        }
        [HttpGet("{lowPrice}")]
        public async Task<ActionResult<GeneralRespons>> GetByPraicelargthanAsync(float lowPrice)
        {
            if (lowPrice < 0)
            {
                return BadRequest(new { message = "highPrice or lowPrice not Exit " });
            }
            var response = await _productMangare.GetByPraicelargthanAsync( lowPrice);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        //Task<GeneralRespons> GetByPraicelessthanAsync(float highPrice);
        [HttpGet("{lowPrice}")]
        public async Task<ActionResult<GeneralRespons>> GetByPraiceLessthanAsync(float largPrice)
        {
            if (largPrice < 0)
            {
                return BadRequest(new { message = "highPrice or lowPrice not Exit " });
            }
            var response = await _productMangare.GetByPraicelessthanAsync(largPrice);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<GeneralRespons>> Add(AddproductDto model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _productMangare.AddAsync(model);
            if (!response.Success)
            {
                return NotFound(response);
            }
           
            return CreatedAtAction(nameof(GetById), new { id = (response.Model as Product)?.Id }, response); // Return 201 Created


        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralRespons>> Update(int id,[FromBody] UpdateProductDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            var response = await _productMangare.UpdateAsync(id,model);
            if (!response.Success)
            {
                return NotFound(response); 
            }

            return Ok(response); 
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralRespons>> Delete(int id)
        {

            var resppnse = await _productMangare.DeleteAsync(id);
            if(!resppnse.Success)
            {
                return BadRequest(resppnse);
            }
            return Ok(resppnse);
        }

    }
}
