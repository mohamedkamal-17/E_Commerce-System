using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.DTOs
{
    public class UserRegisterDTO
    {
        public string UserName { get; set; }
        public String Email {  get; set; }
        public String Password { get; set; }
        public string ConfirmPassWord {  get; set; }
        public string Phone { get; set; }
    }
}
