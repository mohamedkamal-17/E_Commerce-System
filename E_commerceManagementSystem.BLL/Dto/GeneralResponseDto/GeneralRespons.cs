using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto
{
    public class GeneralRespons
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
        public List<string>? Errors { get; set; }
        public object? Model { get; set; }
        public int StatusCode { get; set; }


    }
}
