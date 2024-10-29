using System.Linq.Expressions;

namespace E_commerceManagementSystem.DAL.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IQueryable<T>> GetAllAsync();


        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> condition);
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetAll(Expression<Func<T, bool>> condition,
            params Expression<Func<T, object>>[] includes);



        IQueryable<T> GetByConditionAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveChangesAsync();


    }

}
