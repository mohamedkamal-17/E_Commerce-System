using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.DTOs.RoleDto;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceManagementSystem.BLL.Manager.RoleManager
{
    public interface IRoleMangare
    {
        public Task<GeneralRespons> CreateRole([FromBody] RoleAddDTO roleAddDTO);
        public Task<GeneralRespons> AssignRole([FromBody] AssignRoleDTO model);
        //  public  Task<IActionResult> UpdateRole([FromBody] UpdateRoleModel model)
    }
}
