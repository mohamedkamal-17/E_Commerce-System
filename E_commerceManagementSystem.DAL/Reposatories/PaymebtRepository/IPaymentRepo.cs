using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Repositories.Interfaces;

namespace E_commerceManagementSystem.DAL.Reposatories.PaymebtRepository
{
    public interface IPaymentRepo : IRepository<Payment>
    {
        public Payment GetByPaymentIntentId(string paymentIntentId);
    }
}
