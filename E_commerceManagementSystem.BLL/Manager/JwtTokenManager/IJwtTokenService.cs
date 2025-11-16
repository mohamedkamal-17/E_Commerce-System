using E_commerceManagementSystem.BLL.DTOs;
using E_commerceManagementSystem.DAL.Data.Models;

namespace E_commerceManagementSystem.BLL.Manager.JwtTokenManager
{
    public interface IJwtTokenService
    {
        public TokenRespons GenerateJwtToken(ApplicationUser user, IList<string> roles);
    }
}
