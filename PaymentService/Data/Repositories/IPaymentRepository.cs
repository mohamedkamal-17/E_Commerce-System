using E_commerceManagementSystem.DAL.Data.Models;

namespace PaymentService.Data.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> AddAsync(Payment payment);
        Task<Payment?> GetByIdAsync(int id);
        Task UpdateAsync(Payment payment);
        Task AddTransactionAsync(Transaction tx);
        Task SaveChangesAsync();
    }
}
