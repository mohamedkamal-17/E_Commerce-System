using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto
{
    public class GeneralResponsDto
    {
        public bool Successe { get; set; } = false;

        public List<string>? Errors { get; set; }

    }
}
