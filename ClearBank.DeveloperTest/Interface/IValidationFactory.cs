using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClearBank.DeveloperTest.Enums;

namespace ClearBank.DeveloperTest.Interface
{
    public interface IValidationFactory
    {
        IValidator GetValidator(PaymentScheme scheme);
    }
}
