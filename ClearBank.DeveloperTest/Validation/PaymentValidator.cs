using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClearBank.DeveloperTest.Enums;
using ClearBank.DeveloperTest.Interface;
using ClearBank.DeveloperTest.Models;

namespace ClearBank.DeveloperTest.Validation
{
    public abstract class PaymentValidator : IValidator
    {
        private readonly AllowedPaymentSchemes _paymentScheme;

        protected PaymentValidator(AllowedPaymentSchemes paymentScheme)
        {
            _paymentScheme = paymentScheme;
        }

        public virtual bool ValidatePayment(Account account, decimal amount)
        {
            return account != null && account.AllowedPaymentSchemes.HasFlag(_paymentScheme);
        }
    }
}
