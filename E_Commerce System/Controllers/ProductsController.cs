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

        [HttpGet("Products")]
        public async Task<ActionResult> GetAll()
        {

            var products =await _productMangare.GetAllAsync();

            if (products.Count==0)
            {
                var respons = new GeneralRespons();

                foreach (var product in products)
                {

                }
            }
        }



            //    }
            //}
            [HttpGet("{id}")]
        public ActionResult<GeneralRespons> GetById(int id)
        {

            var product = _productMangare.GetByIdAsync(id);

            if (product != null)
            {
                var respons = new GeneralRespons();

                respons.Success = true;
                respons.Model = product;

                return Ok(respons);
            }
            else
                return BadRequest(new GeneralRespons()
                {

                });


        }
        //[HttpGet("{id}")]
        //public ActionResult<GeneralRespons> GetByCategoryName(string name)
        //{



        //}
        //Task AddAsync(T entity);
        //Task UpdateAsync(T entity);
        //Task DeleteAsync(T entity);
        [HttpPost]
        public async Task<ActionResult<GeneralRespons>> Add([FromBody] AddproductDto addProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new GeneralRespons()
                {
                    Success = false,
                    Model = addProduct,
                    Message = "not valid model"

                });
            }
            var response = new GeneralRespons();
            try
            {
                var ProducToDb = _mapper.Map<Product>(addProduct);
                await _productMangare.AddAsync(_mapper.Map<Product>(ProducToDb));

                response.Success = true;
                response.Model = addProduct;
                response.Message = "Product Created Successeed";
                return CreatedAtAction(nameof(GetById), new { ProducToDb.Id }, response);

            }
            catch (Exception ex)
            {

                response.Message += ex.Message;
                response.Model = addProduct;
                return response;



            }


        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateProductDto updateProduct)
        {
            if (!ModelState.IsValid) {

                return BadRequest(new GeneralRespons()
                {
                    Success = false,
                    Model = updateProduct,
                    Message = "Not valid model"
                });
            }
            try
            {
                var productInDb = await _productMangare.GetByIdAsync(id);
                if (productInDb == null)
                {
                    return BadRequest(new GeneralRespons()
                    {
                        Success = false,
                        Model = updateProduct,
                        Message = "no broduct with this Id"
                    });
                }
                await _productMangare.UpdateAsync(_mapper.Map<Product>(updateProduct));
                return CreatedAtAction(nameof(GetById), new { productInDb.Id },
                                                             new { message = "Prodeuct Updated successefliy" });


            }
            catch (Exception ex) {
            
                return StatusCode(StatusCodes.Status500InternalServerError,new  {message= ex.Message });
            }


        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await _productMangare.GetByIdAsync(id);
                if (product != null)
                {
                    await _productMangare.DeleteAsync(product);
                    return NoContent();

                }
                return BadRequest(new { messge = "no product With this Id " });
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    new { message = "An error occurred while deleting the product", error = ex.Message });

            }

        }
    }
}




