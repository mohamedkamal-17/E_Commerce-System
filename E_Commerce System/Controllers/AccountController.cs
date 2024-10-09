using Azure;
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

        public AccountsController(IAccountManager accountManager, RoleManager<IdentityRole> roleManager)
        {
            _AccountManager = accountManager;
            _RoleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterDTO UserRegisterDTO)
        {
            if (!ModelState.IsValid)
            {
                var response = _AccountManager.CreateResponse(false, null, "Invalid model state", 400, ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }

            GeneralRespons result = await _AccountManager.RegisterAsync(UserRegisterDTO);

            if (!result.Success)
                return StatusCode(result.StatusCode, result);

            return Ok(result); // Now returning GeneralRespons directly
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDTO UserLoginDTO)
        {
            if (!ModelState.IsValid)
            {
                var response = _AccountManager.CreateResponse(false, null, "Invalid model state", 400, ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }

            TokenRespons tokenResponse = await _AccountManager.LoginAsync(UserLoginDTO);

            if (tokenResponse != null)
            {
                var response = _AccountManager.CreateResponse(true, tokenResponse, "Login successful", 200);
                return Ok(response);
            }

            var failedResponse = _AccountManager.CreateResponse(false, null, "Invalid username or password", 400);
            return BadRequest(failedResponse);
        }
    }
}
