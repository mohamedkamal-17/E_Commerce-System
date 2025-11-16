using System.ComponentModel.DataAnnotations;

namespace E_commerceManagementSystem.BLL.Dtos.OtpDto
{
    public class ResetPasswordRequestDto
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Otp { get; set; }
        [DataType(DataType.Password)]
        [MinLength(3, ErrorMessage = "Password must be at least 3 characters long.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
