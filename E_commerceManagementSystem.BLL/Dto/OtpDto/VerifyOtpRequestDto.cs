using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Dtos.OtpDto
{
	public class VerifyOtpRequestDto
	{
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		public string Otp { get; set; }
	}
}
