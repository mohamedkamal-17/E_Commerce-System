using Azure;
using E_commerceManagementSystem.BLL.DTOs;
using  E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 namespace E_commerceManagementSystem.BLL.Manager
{
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenService _jwtTokenService;

        public AccountManager(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signinManager,
            RoleManager<IdentityRole> roleManager,
            IJwtTokenService JwtTokenService)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _roleManager = roleManager;
            _jwtTokenService = JwtTokenService;
        }

      
        public async Task<GeneralRespons> RegisterAsync(UserRegisterDTO UserRegister)
        {
            
            ApplicationUser user = new ApplicationUser();
            user.UserName = UserRegister.UserName;
            user.Email = UserRegister.Email;

            var Response = new GeneralRespons();
            var result =  await _userManager.CreateAsync(user, UserRegister.Password);
            if(result.Succeeded)
            {


                Response.Successe = true;
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
                if(result.Succeeded)
                {
                   var rols = await _userManager.GetRolesAsync(user);
                    TokenRespons tokenRespons= _jwtTokenService.GenerateJwtToken(user, rols);

                    return tokenRespons;

                }
                return null;
            }
           
            return null ;
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
    }

}
