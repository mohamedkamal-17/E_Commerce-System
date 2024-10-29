using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;

namespace E_commerceManagementSystem.BLL.Manager.EmailManager
{
    public interface IEmailManager
    {
        Task<GeneralAccountResponse> SendEmailAsync(string recipientEmail, string subject, string body);

    }
}
