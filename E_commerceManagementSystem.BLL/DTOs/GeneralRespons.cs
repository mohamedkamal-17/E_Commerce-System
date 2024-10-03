using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.DTOs
{
    public class GeneralRespons
    {
        public bool Successe { get; set; } = false;

        public List<string>? Errors { get; set; }

    }
}
