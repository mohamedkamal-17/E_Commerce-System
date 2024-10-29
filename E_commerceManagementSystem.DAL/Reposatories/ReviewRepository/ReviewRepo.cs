using E_commerceManagementSystem.DAL.Data.Dphelper;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.GeneralRepository;

namespace E_commerceManagementSystem.DAL.Reposatories.ReviewRepository
{
    public class ReviewRepo : Repository<Review>, IReviewRepo
    {
        private readonly ApplicationDbContext _context;
        public ReviewRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
