using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.CategoryDto;
using E_commerceManagementSystem.BLL.Dto.ShippingDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.CategoryManger;
using E_commerceManagementSystem.BLL.Manager.ShippingManger;
using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase { 
    private readonly IShippingManger _shippingManger;
    private readonly IMapper _mapper;

      public  ShippingController(IShippingManger ShippingManger, IMapper mapper)
    {
        _shippingManger = ShippingManger;
        _mapper = mapper;
    }



    [HttpGet]
    public async Task<ActionResult<GeneralRespons>> GetAll()
    {

        var response = await _shippingManger.GetAllAsync();
        if (!response.Success)
        {
            return NotFound(response);
        }
        return Ok(response);


    }



    [HttpGet("{id}")]
    public async Task<ActionResult<GeneralRespons>> GetById(int id)
    {
        var response = await _shippingManger.GetByIdAsync(id);
        if (!response.Success)
        {
            return NotFound(response);
        }
        return Ok(response);
    }

   

    [HttpPost]
    public async Task<ActionResult<GeneralRespons>> Add(AddShippingDto model)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var response = await _shippingManger.AddAsync(model);
        if (!response.Success)
        {
            return NotFound(response);
        }

        return CreatedAtAction(nameof(GetById), new { id = (response.Model as Product)?.Id }, response); // Return 201 Created


    }
  
    [HttpPut("{id}")]
    public async Task<ActionResult<GeneralRespons>> Update(int id, [FromBody] UpdateShippingDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _shippingManger.UpdateAsync(id, model);
        if (!response.Success)
        {
            return NotFound(response);
        }

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<GeneralRespons>> Delete(int id)
    {

        var resppnse = await _shippingManger.DeleteAsync(id);
        if (!resppnse.Success)
        {
            return BadRequest(resppnse);
        }
        return Ok(resppnse);
    }

}
}


