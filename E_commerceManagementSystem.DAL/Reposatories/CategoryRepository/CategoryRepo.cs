using E_commerceManagementSystem.DAL.Data.Dphelper;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.GeneralRepository;

namespace E_commerceManagementSystem.DAL.Reposatories.CategoryRepository
{
    public class CategoryRepo : Repository<Category>, ICategoryRepo
    {
        public CategoryRepo(ApplicationDbContext context) : base(context) { }
    }
}
