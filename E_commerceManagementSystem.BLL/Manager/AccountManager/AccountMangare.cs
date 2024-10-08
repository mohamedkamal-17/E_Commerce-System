using E_commerceManagementSystem.BLL.DTOs;
using E_commerceManagementSystem.BLL.DTOs.AccountDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.JwtTokenManager;
using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using E_commerceManagementSystem.BLL.Dtos.OtpDto.OtpDto;
using E_commerceManagementSystem.BLL.Dtos.OtpDto;
using E_commerceManagementSystem.BLL.Manager.EmailManager;
using E_commerceManagementSystem.BLL.Manager.OtpManager;

namespace E_commerceManagementSystem.BLL.Manager.AccountManager
{
    public class AccountMangare : IAccountManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IConfiguration _configuration;
        private readonly IOtpManager _otpManager;
        private readonly IMemoryCache _cache;
        private readonly IEmailManager _emailManager;

        public AccountMangare(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signinManager,
            RoleManager<IdentityRole> roleManager,
            IJwtTokenService JwtTokenService,
            IConfiguration configuration,
            IOtpManager otpManager,
            IMemoryCache cache,
            IEmailManager emailManager)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _roleManager = roleManager;
            _jwtTokenService = JwtTokenService;
            _configuration = configuration;
            _otpManager = otpManager;
            _cache = cache;
            _emailManager = emailManager;
        }


        public async Task<GeneralRespons> RegisterAsync(UserRegisterDTO UserRegister)
        {

            ApplicationUser user = new ApplicationUser();
            user.UserName = UserRegister.UserName;
            user.Email = UserRegister.Email;

            var Response = new GeneralRespons();
            var result = await _userManager.CreateAsync(user, UserRegister.Password);
            if (result.Succeeded)
            {
                Response.Success = true;
                return Response;
            }
            foreach (var error in result.Errors)
            {
                Response.Errors.Add(error.Description);
            }
            return Response;
        }
        public async Task<TokenRespons> LoginAsync(UserLoginDTO loginDTO)
        {

            var user = await _userManager.FindByNameAsync(loginDTO.UserName);

            if (user != null)
            {
                var result = await _signinManager.PasswordSignInAsync
                                           (user, loginDTO.Password, false, false);
                if (result.Succeeded)
                {
                    var rols = await _userManager.GetRolesAsync(user);
                    TokenRespons tokenRespons = _jwtTokenService.GenerateJwtToken(user, rols);

                    return tokenRespons;

                }
                return null;
            }

            return null;
        }

        public async Task LogOutAsync()
        {
            await _signinManager.SignOutAsync();
        }
        public Task<UserRegisterDTO> LoginAsync(UserRegisterDTO loginVM)
        {
            throw new NotImplementedException();
        }

        public Task<UserRegisterDTO> CreateRoleAsync(UserRegisterDTO roleVM)
        {
            throw new NotImplementedException();
        }

        public Task<UserRegisterDTO> AssignRoleToUserAsync(UserRegisterDTO roleToUserVM)
        {
            throw new NotImplementedException();
        }

        public async Task<GeneralAccountResponse> SendOtpForPasswordReset(SendOtpRequestDto dto)
        {
            GeneralAccountResponse GeneralAccountResponse = new GeneralAccountResponse();

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                GeneralAccountResponse.IsSucceeded = false;
                GeneralAccountResponse.Message = "User not found";
                return GeneralAccountResponse;
            }

            var otpCode = await _otpManager.GenerateOtpAsync(dto.Email);


            var emailResponse = await _emailManager.SendEmailAsync(user.Email, "Your Password Reset OTP Code", $"Your OTP code for resetting your password is: {otpCode}");
            if (!emailResponse.IsSucceeded)
            {
                return emailResponse; // Return the email error if sending failed
            }

            GeneralAccountResponse.Message = "OTP sent successfully.";
            GeneralAccountResponse.IsSucceeded = emailResponse.IsSucceeded;
            return GeneralAccountResponse;
        }

        public async Task<GeneralAccountResponse> VerifyOtp(VerifyOtpRequestDto dto)
        {
            GeneralAccountResponse GeneralAccountResponse = new GeneralAccountResponse();

            // Retrieve email from cache using OTP
            if (!_cache.TryGetValue($"{dto.Email}_Verified", out string storedOtp) || storedOtp != dto.Otp)
            {
                GeneralAccountResponse.IsSucceeded = false;
                GeneralAccountResponse.Message = "Invalid or expired OTP";
                return GeneralAccountResponse;
            }

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                GeneralAccountResponse.IsSucceeded = false;
                GeneralAccountResponse.Message = "User not found";
                return GeneralAccountResponse;
            }

            _cache.Set($"{dto.Email}_Verified", true, TimeSpan.FromMinutes(10));

            GeneralAccountResponse.IsSucceeded = true;
            GeneralAccountResponse.Message = "OTP verified successfully. You can now reset your password.";
            return GeneralAccountResponse;
        }

        public async Task<GeneralAccountResponse> ResetPasswordWithOtp(ResetPasswordRequestDto dto)
        {
            GeneralAccountResponse GeneralAccountResponse = new GeneralAccountResponse();

            if (!_cache.TryGetValue($"{dto.Email}_Verified", out bool otpVerified) || !otpVerified)
            {
                GeneralAccountResponse.IsSucceeded = false;
                GeneralAccountResponse.Message = "Invalid or expired OTP";
                return GeneralAccountResponse;
            }

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                GeneralAccountResponse.IsSucceeded = false;
                GeneralAccountResponse.Message = "User not found";
                return GeneralAccountResponse;
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, dto.Password);
            if (!result.Succeeded)
            {
                GeneralAccountResponse.IsSucceeded = false;
                GeneralAccountResponse.Message = string.Join(", ", result.Errors.Select(e => e.Description));
                return GeneralAccountResponse;
            }

            await _otpManager.RemoveOtpAsync(dto.Email);

            var claims = await _userManager.GetClaimsAsync(user);
            var rols = await _userManager.GetRolesAsync(user);


            //  GeneralAccountResponse = GeneralToken(claims);
             var  token =  _jwtTokenService.GenerateJwtToken(user, rols);
            GeneralAccountResponse.Token = token.Token;

            GeneralAccountResponse.ExpireDate = token.Exp;
            GeneralAccountResponse.IsSucceeded = true;
            GeneralAccountResponse.Message = "Password reset successfully";
            return GeneralAccountResponse;
        }

    /*    private GeneralAccountResponse GeneralToken(IList<Claim> claims)
        {

            var securityKeyOfString = _configuration.GetSection("Key").Value;
            var securityKeyOfBytes = Encoding.ASCII.GetBytes(securityKeyOfString);
            var securityKey = new SymmetricSecurityKey(securityKeyOfBytes);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var expireDate = DateTime.Now.AddDays(2);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                claims: claims,
                expires: expireDate,
                signingCredentials: signingCredentials
                );

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

            return new GeneralAccountResponse
            {
                Token = token,
                ExpireDate = expireDate
            };
        }*/
    }

}
