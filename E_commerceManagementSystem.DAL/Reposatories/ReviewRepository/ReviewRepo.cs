﻿using E_commerceManagementSystem.DAL.Data.Dphelper;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.GeneralRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.DAL.Reposatories.ReviewRepository
{
    public class ReviewRepo : Repository<Review>, IReviewRepo
    {
        private readonly ApplicationDbContext _context;
        public ReviewRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IQueryable<Review>> GetByProductIdAsync(int productId)
        {
            return _context.Reviews.Where(x => x.ProductId == productId).AsNoTracking();
        }

        public async Task<IQueryable<Review>> GetByUserIdAsync(string userId)
        {
            return _context.Reviews.Where(x => x.UserId == userId).AsNoTracking();
        }
    }
}
