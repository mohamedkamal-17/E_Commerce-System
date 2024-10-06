﻿using AutoMapper;
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

        CategoryController(ICategoryManger CategoryManger, IMapper mapper)
        {
            _categoryManger = CategoryManger;
            _mapper = mapper;
        }



        [HttpGet]
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
        public async Task<ActionResult<GeneralRespons>> GetById(int id)
        {
            var response = await _categoryManger.GetByIdAsync(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        //Task<GeneralRespons> GetAllAsync();
        //Task<GeneralRespons> GetByIdAsync(int id);


        //Task<GeneralRespons> AddAsync(TAddDto dto);

        [HttpPost]
        public async Task<ActionResult<GeneralRespons>> Add(AddCategoryDTO model)
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

            return CreatedAtAction(nameof(GetById), new { id = (response.Model as Product)?.Id }, response); // Return 201 Created


        }
        //Task<GeneralRespons> UpdateAsync(TUpdateDto dto);
        [HttpPut("{id}")]
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
        public async Task<ActionResult<GeneralRespons>> Delete(int id)
        {

            var resppnse = await _categoryManger.DeleteAsync(id);
            if (!resppnse.Success)
            {
                return BadRequest(resppnse);
            }
            return Ok(resppnse);
        }

    }
}
