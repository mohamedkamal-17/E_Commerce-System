using E_commerceManagementSystem.BLL.DTOs;
using E_commerceManagementSystem.BLL.DTOs.AccountDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.AccountManager;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using E_commerceManagementSystem.BLL.Dtos.OtpDto.OtpDto;
using E_commerceManagementSystem.BLL.Dtos.OtpDto;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountManager _accountManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountsController(IAccountManager accountManager,RoleManager<IdentityRole> roleManager)
        {
            _accountManager = accountManager;
            _roleManager = roleManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegistarAsync([FromBody]UserRegisterDTO UserRegisterDTO )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            GeneralRespons result=await _accountManager.RegisterAsync(UserRegisterDTO);
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

            TokenRespons result = await _accountManager.LoginAsync(UserLoginDTO);
            if (result != null)
            {
                return Ok(result);
            }
                return BadRequest("User name or Password is not valid");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] SendOtpRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ModelState, StatusCode = 400 });
            }
            var response = await _accountManager.SendOtpForPasswordReset(dto);
            if (!response.IsSucceeded)
            {
                return BadRequest(new { response.Message, StatusCode = 400 });
            }
            return Ok(new { response.Message, statusCode = 200 });
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ModelState, StatusCode = 400 });
            }

            var response = await _accountManager.VerifyOtp(dto);
            if (!response.IsSucceeded)
            {
                return BadRequest(new { response.Message, StatusCode = 400 });
            }
            return Ok(new { response.Message, statusCode = 200 });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordWithOtp([FromBody] ResetPasswordRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ModelState, StatusCode = 400 });
            }

            var response = await _accountManager.ResetPasswordWithOtp(dto);
            if (!response.IsSucceeded)
            {
                return BadRequest(new { response.Message, StatusCode = 400 });
            }

            return Ok(new
            {
                token = response.Token,
                expireDate = response.ExpireDate,
                message = response.Message,
                StatusCode = 200
            });
        }

    }
}
