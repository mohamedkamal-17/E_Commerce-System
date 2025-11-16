using AutoMapper;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.DTOs.RoleDto;
using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        private ActionResult<GeneralRespons> HandleResponse(GeneralRespons response)
        {
            if (!response.Success)
            {
                return StatusCode((int)response.StatusCode, response); // Return the appropriate status code and response
            }
            return Ok(response); // Return 200 OK if successful
        }

        [HttpPost("create")]
        public async Task<ActionResult<GeneralRespons>> CreateRoleAsync([FromBody] RoleAddDTO model)
        {
          
            var roleExists = await _roleManager.RoleExistsAsync(model.RoleName);
            if (roleExists)
                return BadRequest(new GeneralRespons { Success = false, Message = "Role already exists." });

            var role = new IdentityRole(model.RoleName);
            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
                return BadRequest(new GeneralRespons { Success = false, Message = "Error creating role.", Model = result.Errors });

            var response = new GeneralRespons { Success = true, Message = "Role created successfully." };
            return HandleResponse(response);
        }

        [HttpPost("assign")]
        public async Task<ActionResult<GeneralRespons>> AssignRoleAsync([FromBody] AssignRoleDTO model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return NotFound(new GeneralRespons { Success = false, Message = "User not found." });

            var roleExists = await _roleManager.RoleExistsAsync(model.RoleName);
            if (!roleExists)
                return NotFound(new GeneralRespons { Success = false, Message = "Role not found." });

            var result = await _userManager.AddToRoleAsync(user, model.RoleName);
            if (!result.Succeeded)
                return BadRequest(new GeneralRespons { Success = false, Message = "Error assigning role.", Model = result.Errors });

            var response = new GeneralRespons { Success = true, Message = "Role assigned successfully." };
            return HandleResponse(response);
        }

        [HttpPut("update")]
        public async Task<ActionResult<GeneralRespons>> UpdateRoleAsync([FromBody] UpdateRoleDTO model)
        {
         
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            if (role == null)
                return NotFound(new GeneralRespons { Success = false, Message = "Role not found." });

            role.Name = model.NewRoleName;
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
                return BadRequest(new GeneralRespons { Success = false, Message = "Error updating role.", Model = result.Errors });

            var response = new GeneralRespons { Success = true, Message = "Role updated successfully." };
            return HandleResponse(response);
        }

        [HttpDelete("delete/{roleId}")]
        public async Task<ActionResult<GeneralRespons>> DeleteRoleAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
                return NotFound(new GeneralRespons { Success = false, Message = "Role not found." });

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
                return BadRequest(new GeneralRespons { Success = false, Message = "Error deleting role.", Model = result.Errors });

            var response = new GeneralRespons { Success = true, Message = "Role deleted successfully." };
            return HandleResponse(response);
        }
    }

    // Models
   
}
