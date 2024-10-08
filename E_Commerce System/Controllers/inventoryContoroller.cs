﻿using E_commerceManagementSystem.BLL.Dto.InventoryDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.InventoryManager;
using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryManagre _inventoryManager;

        public InventoryController(IInventoryManagre inventoryManager)
        {
            _inventoryManager = inventoryManager;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralRespons>> GetAll()
        {
            var response = await _inventoryManager.GetAllAsync();
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("{id:int}")] // Route constraint to enforce int type
        public async Task<ActionResult<GeneralRespons>> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "ID should be greater than zero" });
            }
            var response = await _inventoryManager.GetByIdAsync(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("product/{productId:int}")] // More descriptive route to avoid conflicts
        public async Task<ActionResult<GeneralRespons>> GetByProductId(int productId)
        {
            if (productId <= 0)
            {
                return BadRequest(new { message = "Product ID should be greater than zero" });
            }
            var response = await _inventoryManager.GetByProductId(productId);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<GeneralRespons>> Create([FromBody] AddInventoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _inventoryManager.AddAsync(model);
            if (!response.Success)
            {
                return NotFound(response);
            }

            return CreatedAtAction(nameof(GetById), new { id = (response.Model as Inventory)?.Id }, response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GeneralRespons>> Update(int id, [FromBody] UpdateInventoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _inventoryManager.UpdateAsync(id, model);
            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<GeneralRespons>> Delete(int id)
        {
            var response = await _inventoryManager.DeleteAsync(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}