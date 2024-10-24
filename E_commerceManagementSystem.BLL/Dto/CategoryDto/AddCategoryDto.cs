using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Dto.CategoryDto
{
    public class AddCategoryDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]

        public string Description { get; set; }

        public string ImgUrel { get; set; }

    }
}
