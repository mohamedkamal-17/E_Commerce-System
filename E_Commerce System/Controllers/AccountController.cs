using E_commerceManagementSystem.BLL.DTOs;
using E_commerceManagementSystem.BLL.DTOs.AccountDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.AccountManager;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountManager _AccountManager;
        private readonly RoleManager<IdentityRole> _RoleManager;

        public AccountsController(IAccountManager accountManager,RoleManager<IdentityRole> roleManager)
        {
            _AccountManager = accountManager;
            _RoleManager = roleManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegistarAsync([FromBody]UserRegisterDTO UserRegisterDTO )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            GeneralRespons result=await _AccountManager.RegisterAsync(UserRegisterDTO);
            if (!result.Success)
                return BadRequest(result);

            else
                return Ok("User registered successfully!");
        }

        [HttpPost("login")]
       
        public async Task<IActionResult>LoginAsync([FromBody] UserLoginDTO UserLoginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            TokenRespons result = await _AccountManager.LoginAsync(UserLoginDTO);
            if (result != null)
            {
                return Ok(result);
            }
                return BadRequest("User name or Password is not valid");
        }
    }
}
