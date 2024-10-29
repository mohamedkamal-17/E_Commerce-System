using System.ComponentModel.DataAnnotations;

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
