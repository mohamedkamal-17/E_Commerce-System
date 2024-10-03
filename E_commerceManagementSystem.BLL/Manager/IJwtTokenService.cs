using E_commerceManagementSystem.BLL.DTOs;
using E_commerceManagementSystem.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Manager
{
    public interface IJwtTokenService
    {
        public TokenRespons GenerateJwtToken(ApplicationUser user, IList<string> roles);
    }
}
