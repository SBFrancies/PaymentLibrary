using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClearBank.DeveloperTest.Enums;
using ClearBank.DeveloperTest.Models;

namespace ClearBank.DeveloperTest.Validation
{
    public class ChapsPaymentValidator : PaymentValidator
    {
        public ChapsPaymentValidator() : base(AllowedPaymentSchemes.Chaps)
        {
        }

        public override bool ValidatePayment(Account account, decimal amount)
        {
            return base.ValidatePayment(account, amount) && account.Status == AccountStatus.Live;
        }
    }
}
