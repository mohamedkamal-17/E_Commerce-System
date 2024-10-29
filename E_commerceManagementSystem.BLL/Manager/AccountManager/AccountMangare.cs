using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.CartDto;
using E_commerceManagementSystem.BLL.Dtos.OtpDto;
using E_commerceManagementSystem.BLL.Dtos.OtpDto.OtpDto;
using E_commerceManagementSystem.BLL.DTOs;
using E_commerceManagementSystem.BLL.DTOs.AccountDto;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;
using E_commerceManagementSystem.BLL.Manager.EmailManager;
using E_commerceManagementSystem.BLL.Manager.JwtTokenManager;
using E_commerceManagementSystem.BLL.Manager.OtpManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.CartRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace E_commerceManagementSystem.BLL.Manager.AccountManager
{
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IConfiguration _configuration;
        private readonly IOtpManager _otpManager;
        private readonly IMemoryCache _cache;
        private readonly IEmailManager _emailManager;
        private readonly ICartRepo _cartRepo;
        private readonly IMapper _mapper;
        public AccountManager(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signinManager,
            RoleManager<IdentityRole> roleManager,
            IJwtTokenService jwtTokenService, // Corrected constructor parameter
            IConfiguration configuration,
            IOtpManager otpManager,
            IMemoryCache cache,
            IEmailManager emailManager,
            ICartRepo cartRepo,
            IMapper mapper)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _roleManager = roleManager;
            _jwtTokenService = jwtTokenService;
            _configuration = configuration; // Removed duplicate assignment
            _otpManager = otpManager;
            _cache = cache;
            _emailManager = emailManager;
            _cartRepo = cartRepo;
            _mapper = mapper;
        }

        public GeneralRespons CreateResponse(bool success, object? model, string message, int statusCode, List<string>? errors = null)
        {
            return new GeneralRespons
            {
                Success = success,
                Model = model,
                Message = message,
                StatusCode = statusCode,
                Errors = errors ?? new List<string>()
            };
        }

        public async Task<GeneralRespons> RegisterAsync(UserRegisterDTO userRegister)
        {
            var response = new GeneralRespons();

            var userExists = await _userManager.FindByEmailAsync(userRegister.Email);
            if (userExists != null)
            {
                return CreateResponse(false, null, "this email already exists.", 400, response.Errors);
            }

            var existingUser = await _userManager.FindByNameAsync(userRegister.UserName);
            if (existingUser != null)
            {
                return CreateResponse(false, null, "this user name already exists, please enter another name", 400, response.Errors);

            }
            var user = new ApplicationUser
            {
                UserName = userRegister.UserName,
                Email = userRegister.Email
            };

            var result = await _userManager.CreateAsync(user, userRegister.Password);

            if (result.Succeeded)
            {

                var roleResult = await _userManager.AddToRoleAsync(user, "User");

                if (!roleResult.Succeeded)
                {
                    foreach (var error in roleResult.Errors)
                    {
                        response.Errors.Add(error.Description);
                    }
                    return CreateResponse(false, null, "Failed to assign role to the user.", 400, response.Errors);
                }

                //add cart to user when he register to app
                var addCartDto = new AddCartDto
                {
                    UserId = user.Id
                };

                var cart = _mapper.Map<Cart>(addCartDto); // Map the AddCartDto to the entity

                await _cartRepo.AddAsync(cart);


                await _cartRepo.SaveChangesAsync();

                return CreateResponse(true, null, "User registered successfully.", 201); // Created
            }


            // Removed extra Response variable
            foreach (var error in result.Errors)
            {
                response.Errors.Add(error.Description);
            }
            return CreateResponse(false, null, "User registration failed.", 400, response.Errors); // Bad Request
        }

        public async Task<GeneralAccountResponse?> LoginAsync(UserLoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user != null)
            {
                var result = await _signinManager.PasswordSignInAsync(user, loginDTO.Password, false, false);
                if (result.Succeeded)
                {
                    var roles = (await _userManager.GetRolesAsync(user)).ToList();
                    TokenRespons tokenResponse = _jwtTokenService.GenerateJwtToken(user, roles);


                    var cart = _cartRepo.GetAll(x => x.UserId == user.Id).ToList().FirstOrDefault();

                    return new GeneralAccountResponse
                    {
                        Role = roles,
                        Token = tokenResponse.Token,
                        ExpireDate = tokenResponse.Exp,
                        Email = user.Email,
                        Id = user.Id,
                        UserName = user.UserName,
                        CartId = cart.Id
                    };
                }
                return null;
            }

            return null; // User not found
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
            GeneralAccountResponse generalAccountResponse = new GeneralAccountResponse();

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                generalAccountResponse.IsSucceeded = false;
                generalAccountResponse.Message = "User not found";
                return generalAccountResponse;
            }

            var otpCode = await _otpManager.GenerateOtpAsync(dto.Email);

            var emailResponse = await _emailManager.SendEmailAsync(user.Email, "Your Password Reset OTP Code", $"Your OTP code for resetting your password is: {otpCode}");
            if (!emailResponse.IsSucceeded)
            {
                return emailResponse; // Return the email error if sending failed
            }

            generalAccountResponse.Message = "OTP sent successfully.";
            generalAccountResponse.IsSucceeded = emailResponse.IsSucceeded;
            return generalAccountResponse;
        }

        public async Task<GeneralAccountResponse> VerifyOtp(VerifyOtpRequestDto dto)
        {
            GeneralAccountResponse generalAccountResponse = new GeneralAccountResponse();

            // Retrieve email from cache using OTP
            if (!_cache.TryGetValue($"{dto.Email}_Verified", out string storedOtp) || storedOtp != dto.Otp)
            {
                generalAccountResponse.IsSucceeded = false;
                generalAccountResponse.Message = "Invalid or expired OTP";
                return generalAccountResponse;
            }

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                generalAccountResponse.IsSucceeded = false;
                generalAccountResponse.Message = "User not found";
                return generalAccountResponse;
            }

            _cache.Set($"{dto.Email}_Verified", true, TimeSpan.FromMinutes(10));

            generalAccountResponse.IsSucceeded = true;
            generalAccountResponse.Message = "OTP verified successfully. You can now reset your password.";
            return generalAccountResponse;
        }

        public async Task<GeneralAccountResponse> ResetPasswordWithOtp(ResetPasswordRequestDto dto)
        {
            GeneralAccountResponse generalAccountResponse = new GeneralAccountResponse();

            if (!_cache.TryGetValue($"{dto.Email}_Verified", out bool otpVerified) || !otpVerified)
            {
                generalAccountResponse.IsSucceeded = false;
                generalAccountResponse.Message = "Invalid or expired OTP";
                return generalAccountResponse;
            }

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                generalAccountResponse.IsSucceeded = false;
                generalAccountResponse.Message = "User not found";
                return generalAccountResponse;
            }


            ///////////////////////////////--
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, dto.Password);
            if (!result.Succeeded)
            {
                generalAccountResponse.IsSucceeded = false;
                generalAccountResponse.Message = string.Join(", ", result.Errors.Select(e => e.Description));
                return generalAccountResponse;
            }

            await _otpManager.RemoveOtpAsync(dto.Email);

            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            //  GeneralAccountResponse = GeneralToken(claims);
            var token = _jwtTokenService.GenerateJwtToken(user, roles);
            generalAccountResponse.Token = token.Token;

            generalAccountResponse.ExpireDate = token.Exp;
            generalAccountResponse.IsSucceeded = true;
            generalAccountResponse.Message = "Password reset successfully";
            return generalAccountResponse;
        }


    }
}
