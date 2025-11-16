using System.ComponentModel.DataAnnotations;

namespace E_commerceManagementSystem.BLL.Dtos.OtpDto
{
    public class VerifyOtpRequestDto
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Otp { get; set; }
    }
}
