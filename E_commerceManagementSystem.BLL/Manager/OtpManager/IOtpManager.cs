using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerceManagementSystem.DAL.Data.Models;

namespace E_commerceManagementSystem.BLL.Manager.OtpManager
{
	public interface IOtpManager
	{
		Task<string> GenerateOtpAsync(string email);
		Task RemoveOtpAsync(string email);
	}
}
