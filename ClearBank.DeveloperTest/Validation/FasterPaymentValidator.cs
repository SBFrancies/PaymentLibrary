using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClearBank.DeveloperTest.Enums;
using ClearBank.DeveloperTest.Models;

namespace ClearBank.DeveloperTest.Validation
{
    public class FasterPaymentValidator : PaymentValidator
    {
        public FasterPaymentValidator() : base(AllowedPaymentSchemes.FasterPayments)
        {
        }

        public override bool ValidatePayment(Account account, decimal amount)
        {
            return base.ValidatePayment(account, amount) && account.Balance >= amount;
        }
    }
}
