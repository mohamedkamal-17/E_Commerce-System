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
        GeneralRespons CreateResponse(bool success, object? model, string message, int statusCode, List<string>? errors = null)


;        Task<GeneralRespons> RegisterAsync(UserRegisterDTO registerVM);
        Task<TokenRespons> LoginAsync(UserLoginDTO UserLoginDTO);
        Task LogOutAsync();
    }

}
