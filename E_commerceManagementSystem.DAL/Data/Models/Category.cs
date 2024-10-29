namespace E_commerceManagementSystem.DAL.Data.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgUrel { get; set; }
        public bool IsDeleted { get; set; } = false;


        public ICollection<Product> Products { get; set; } // Navigation prop
    }
}
