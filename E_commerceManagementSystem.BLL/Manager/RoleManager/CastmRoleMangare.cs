using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.DTOs.RoleDto;
using E_commerceManagementSystem.BLL.Manager.RoleManager;
using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceManagementSystem.BLL.Manager.Classes
{
    public class CastmRoleMangare : IRoleMangare
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CastmRoleMangare(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Updated private CreateResponse method to include the 'Model' parameter
        private GeneralRespons CreateResponse(bool success, object? model, string message, int statusCode, List<string>? errors = null)
        {
            return new GeneralRespons
            {
                Success = success,
                Model = model,  // Include the 'Model' here
                Message = message,
                StatusCode = statusCode,
                Errors = errors ?? new List<string>()
            };
        }

        public async Task<GeneralRespons> AssignRole([FromBody] AssignRoleDTO model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user != null)
            {
                var roleExists = await _roleManager.RoleExistsAsync(model.RoleName);
                if (!roleExists)
                {
                    var result = await _userManager.AddToRoleAsync(user, model.RoleName);
                    if (result.Succeeded)
                    {
                        return CreateResponse(true, user, "Role assigned successfully.", 200); // Include user in the Model
                    }

                    var errors = new List<string>();
                    foreach (var error in result.Errors)
                    {
                        errors.Add(error.Description);
                    }
                    return CreateResponse(false, null, "Failed to assign role.", 500, errors);
                }

                return CreateResponse(false, null, "Role already exists.", 409);
            }

            return CreateResponse(false, null, "User not found.", 404);
        }

        public async Task<GeneralRespons> CreateRole([FromBody] RoleAddDTO roleAddDTO)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleAddDTO.RoleName);
            if (roleExists)
            {
                return CreateResponse(false, null, "Role already exists.", 409);
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(roleAddDTO.RoleName));
            if (result.Succeeded)
            {
                var createdRole = await _roleManager.FindByNameAsync(roleAddDTO.RoleName); // Fetch the created role to return in the response
                return CreateResponse(true, createdRole, "Role created successfully.", 201); // Include created role in the Model
            }

            var errors = new List<string>();
            foreach (var error in result.Errors)
            {
                errors.Add(error.Description);
            }

            return CreateResponse(false, null, "Failed to create role.", 500, errors);
        }
    }
}
