using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClearBank.DeveloperTest.Enums;
using ClearBank.DeveloperTest.Interface;
using ClearBank.DeveloperTest.Validation;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.ValidationTests
{
    public class ValidationFactoryTests
    {
        [Fact]
        public void BacsSchemeReturnsBacsValidator()
        {
            ValidationFactory factory = GetSystemUnderTest();

            IValidator validator = factory.GetValidator(PaymentScheme.Bacs);

            Assert.IsType<BacsPaymentValidator>(validator);
        }

        [Fact]
        public void ChapsSchemeReturnsBacsValidator()
        {
            ValidationFactory factory = GetSystemUnderTest();

            IValidator validator = factory.GetValidator(PaymentScheme.Chaps);

            Assert.IsType<ChapsPaymentValidator>(validator);
        }

        [Fact]
        public void FasterSchemeReturnsBacsValidator()
        {
            ValidationFactory factory = GetSystemUnderTest();

            IValidator validator = factory.GetValidator(PaymentScheme.FasterPayments);

            Assert.IsType<FasterPaymentValidator>(validator);
        }

        private ValidationFactory GetSystemUnderTest()
        {
            return new ValidationFactory();
        }
    }
}
