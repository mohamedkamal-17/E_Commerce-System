using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto;

namespace E_commerceManagementSystem.BLL.Manager.EmailManager
{
    public interface IEmailManager
    {
		Task<GeneralAccountResponse> SendEmailAsync(string recipientEmail, string subject, string body);

	}
}
