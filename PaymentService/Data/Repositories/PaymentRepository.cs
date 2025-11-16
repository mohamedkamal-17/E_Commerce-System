using E_commerceManagementSystem.DAL.Data.Dphelper;
using E_commerceManagementSystem.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace PaymentService.Data.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _db;
        public PaymentRepository(ApplicationDbContext db) => _db = db;
        public async Task<Payment> AddAsync(Payment payment)
        {
            _db.Payments.Add(payment);
            await _db.SaveChangesAsync();
            return payment;
        }

        public async Task AddTransactionAsync(Transaction tx)
        {
            _db.Transactions.Add(tx);
            await _db.SaveChangesAsync();
        }

        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await _db.Payments.Include(p => p.Transactions).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();

        public async Task UpdateAsync(Payment payment)
        {
            _db.Payments.Update(payment);
            await _db.SaveChangesAsync();
        }
    }
}
