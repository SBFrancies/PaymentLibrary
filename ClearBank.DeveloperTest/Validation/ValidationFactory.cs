using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClearBank.DeveloperTest.Enums;
using ClearBank.DeveloperTest.Interface;

namespace ClearBank.DeveloperTest.Validation
{
    public class ValidationFactory : IValidationFactory
    {
        public IValidator GetValidator(PaymentScheme scheme)
        {
            switch (scheme)
            {
                case PaymentScheme.FasterPayments:
                    return new FasterPaymentValidator();
                case PaymentScheme.Bacs:
                    return new BacsPaymentValidator();
                case PaymentScheme.Chaps:
                    return new ChapsPaymentValidator();
                default:
                    throw new ArgumentOutOfRangeException(nameof(scheme));
            }
        }
    }
}
