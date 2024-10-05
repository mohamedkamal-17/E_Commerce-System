using E_commerceManagementSystem.BLL.DTOs;
using E_commerceManagementSystem.BLL.DTOs.AccountDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager.AccountManager
{
    public interface IAccountManager
    {
        Task<GeneralRespons> RegisterAsync(UserRegisterDTO registerVM);
        Task<TokenRespons> LoginAsync(UserLoginDTO UserLoginDTO);
        Task<UserRegisterDTO> CreateRoleAsync(UserRegisterDTO roleVM);
        Task<UserRegisterDTO> AssignRoleToUserAsync(UserRegisterDTO roleToUserVM);
        Task LogOutAsync();
    }

}
