using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClearBank.DeveloperTest.Enums;
using ClearBank.DeveloperTest.Models;

namespace ClearBank.DeveloperTest.Validation
{
    public class BacsPaymentValidator : PaymentValidator
    {
        public BacsPaymentValidator() : base(AllowedPaymentSchemes.Bacs)
        {           
        }
    }
}
