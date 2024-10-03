using E_commerceManagementSystem.BLL.DTOs;
using E_commerceManagementSystem.BLL.Manager.Interfaces;
using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_commerceManagementSystem.BLL.Manager.Classes
{
    public class CastmRoleMangare : IRoleMangare
    {
        private readonly UserManager<ApplicationUser> _UserManager;

        private readonly RoleManager<IdentityRole> _RoleManager;

        CastmRoleMangare(UserManager<ApplicationUser> userManager,

            RoleManager<IdentityRole> roleManager)
        {
            _UserManager = userManager;

            _RoleManager = roleManager;
        }

        public async Task<GeneralRespons> AssignRole([FromBody] AssignRoleDTO model)
        {
            GeneralRespons respons = new GeneralRespons();

            var user = await _UserManager.FindByIdAsync(model.UserId);
            if (user != null)
            {
                var role = await _RoleManager.RoleExistsAsync(model.RoleName);
                if (!role)
                {
                    var result = await _UserManager.AddToRoleAsync(user, model.RoleName);
                    if (result.Succeeded)
                    {
                        respons.Successe = true;
                        return respons;
                    }
                }
                else
                {
                    respons.Errors.Add("role orledy Exit");
                    return respons;
                }


            }
            respons.Errors.Add("ther is no user with this id");
            return respons;



        }

        public async Task<GeneralRespons> CreateRole([FromBody] RoleAddDTO roleAddDTO)
        {
            GeneralRespons respons = new GeneralRespons();
            var roleExists = await _RoleManager.RoleExistsAsync(roleAddDTO.RoleName);
            if (roleExists)
            {
                respons.Errors.Add("Role already exists.");
                return respons;

            }

            var result = await _RoleManager.CreateAsync(new IdentityRole(roleAddDTO.RoleName));

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    respons.Errors.Add(error.Description);
                }
            }
            respons.Successe = true;
            return respons;
        }

        //public Task<IActionResult> UpdateRole([FromBody] UpdateRoleModel model)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<IActionResult> IRoleMangare.AssignRole(AssignRoleDTO model)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<IActionResult> IRoleMangare.CreateRole(RoleAddDTO roleAddDTO)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
