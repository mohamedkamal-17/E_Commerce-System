using System.ComponentModel.DataAnnotations;

namespace E_commerceManagementSystem.BLL.Dtos.OtpDto.OtpDto
{
    public class SendOtpRequestDto
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
