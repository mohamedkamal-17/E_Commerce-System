using Stripe;

namespace E_commerceManagementSystem.BLL.Manager.PaymentManager
{
    internal class PaymentIntentAutomaticPaymentMethodOptions : PaymentIntentAutomaticPaymentMethodsOptions
    {
        public bool Enabled { get; set; }
        public object AllowRedirects { get; set; }
    }
}