using System.ComponentModel.DataAnnotations;

namespace E_commerceManagementSystem.BLL.DTOs.AccountDto
{
    public class UserLoginDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]

        public string Password { get; set; }
    }
}
