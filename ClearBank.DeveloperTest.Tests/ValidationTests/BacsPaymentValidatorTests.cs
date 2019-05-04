using ClearBank.DeveloperTest.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using ClearBank.DeveloperTest.Enums;
using ClearBank.DeveloperTest.Models;
using Moq;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.ValidationTests
{
    public class BacsPaymentValidatorTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void NullAccountFailsValidation()
        {
            BacsPaymentValidator sut = GetSystemUnderTest();

            bool result = sut.ValidatePayment(null, It.IsAny<decimal>());

            Assert.False(result);
        }

        [Theory]
        [InlineData(AllowedPaymentSchemes.Chaps | AllowedPaymentSchemes.FasterPayments)]
        [InlineData(AllowedPaymentSchemes.Chaps)]
        [InlineData(AllowedPaymentSchemes.FasterPayments)]
        public void DoesNotAllowBacsFailsValidation(AllowedPaymentSchemes schemes)
        {
            BacsPaymentValidator sut = GetSystemUnderTest();
            Account account = _fixture.Create<Account>();
            account.AllowedPaymentSchemes = schemes;

            bool result = sut.ValidatePayment(account, It.IsAny<decimal>());

            Assert.False(result);
        }

        [Theory]
        [InlineData(AllowedPaymentSchemes.Chaps | AllowedPaymentSchemes.FasterPayments | AllowedPaymentSchemes.Bacs)]
        [InlineData(AllowedPaymentSchemes.Chaps | AllowedPaymentSchemes.Bacs)]
        [InlineData(AllowedPaymentSchemes.FasterPayments | AllowedPaymentSchemes.Bacs)]
        [InlineData(AllowedPaymentSchemes.Bacs)]
        public void AllowsBacsPassessValidation(AllowedPaymentSchemes schemes)
        {
            BacsPaymentValidator sut = GetSystemUnderTest();
            Account account = _fixture.Create<Account>();
            account.AllowedPaymentSchemes = schemes;

            bool result = sut.ValidatePayment(account, It.IsAny<decimal>());

            Assert.True(result);
        }

        private BacsPaymentValidator GetSystemUnderTest()
        {
            return new BacsPaymentValidator();
        }
    }
}
