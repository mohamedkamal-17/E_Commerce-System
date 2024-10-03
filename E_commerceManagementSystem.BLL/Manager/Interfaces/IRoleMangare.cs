using E_commerceManagementSystem.BLL.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.Interfaces
{
    public interface IRoleMangare
    {
        public Task<GeneralRespons> CreateRole([FromBody] RoleAddDTO roleAddDTO);
        public Task<GeneralRespons> AssignRole([FromBody] AssignRoleDTO model);
        //  public  Task<IActionResult> UpdateRole([FromBody] UpdateRoleModel model)
    }
}
