using E_commerceManagementSystem.DAL.Data.Dphelper;
using E_commerceManagementSystem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_commerceManagementSystem.DAL.Reposatories.GeneralRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            return _dbSet.AsNoTracking();
        }

        //public async Task<IQueryable<T>> GetAllWithIncludesAsync(params Expression<Func<T, object>>[] includes)
        //{
        //    IQueryable<T> query = _dbSet.AsQueryable();
        //    foreach (var include in includes)
        //    {
        //        query = query.Include(include);
        //    }

        //    return query;
        //}


        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        // 2. Virtual method to get all records with a condition
        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> condition)
        {
            return _dbSet.Where(condition);
        }

        // 3. Virtual method to get all records with includes
        public virtual IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);  // Assuming Include is available for IQueryable
            }
            return query;
        }

        // 4. Virtual method to get all records with a condition and includes
        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet.Where(condition);
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }


        IQueryable<T> IRepository<T>.GetByConditionAsync(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> result = _dbSet.Where(expression);
            return result;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
