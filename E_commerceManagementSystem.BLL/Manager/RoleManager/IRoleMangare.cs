using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.DTOs.RoleDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.RoleManager
{
    public interface IRoleMangare
    {
        public Task<GeneralResponsDto> CreateRole([FromBody] RoleAddDTO roleAddDTO);
        public Task<GeneralResponsDto> AssignRole([FromBody] AssignRoleDTO model);
        //  public  Task<IActionResult> UpdateRole([FromBody] UpdateRoleModel model)
    }
}
