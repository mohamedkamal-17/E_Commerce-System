using E_commerceManagementSystem.DAL.Data.Dphelper;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.GeneralRepository;

namespace E_commerceManagementSystem.DAL.Reposatories.PaymebtRepository
{
    public class PaymentRepo : Repository<Payment>, IPaymentRepo
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Payment GetByPaymentIntentId(string paymentIntentId)
        {
            return _context.Set<Payment>().FirstOrDefault(pa => pa.PaymentIntentId == paymentIntentId);
        }
    }
}
