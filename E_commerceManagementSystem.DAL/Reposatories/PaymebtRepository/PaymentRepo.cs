using E_commerceManagementSystem.DAL.Data.Dphelper;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.GeneralRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.DAL.Reposatories.PaymebtRepository
{
    public class PaymentRepo:Repository<Payment>,IPaymentRepo
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Payment GetByPaymentIntentId(string paymentIntentId)
        {
            return _context.Set<Payment>().FirstOrDefault(pa=>pa.PaymentIntentId== paymentIntentId);
        }
    }
}
