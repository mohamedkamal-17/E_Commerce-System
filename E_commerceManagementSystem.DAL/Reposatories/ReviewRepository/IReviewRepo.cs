using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.DAL.Reposatories.ReviewRepository
{
    public interface IReviewRepo : IRepository<Review>
    {
        Task<IQueryable<Review>> GetByUserIdAsync(string userId);
        Task<IQueryable<Review>> GetByProductIdAsync(int productid);

    }
}
