using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.BLL.Dto.PaymentDto
{
    public class AddIntentPymentRsponsDto
    {
        public string PaymentIntentId { get; set; } // Stripe Payment Intent ID
        public string Status { get; set; }          // Payment status (e.g., succeeded, requires_action)
    }
    }
