using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using ClearBank.DeveloperTest.Enums;
using ClearBank.DeveloperTest.Models;
using ClearBank.DeveloperTest.Validation;
using Moq;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.ValidationTests
{
    public class FasterPaymentValidatorTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void NullAccountFailsValidation()
        {
            FasterPaymentValidator sut = GetSystemUnderTest();

            bool result = sut.ValidatePayment(null, It.IsAny<decimal>());

            Assert.False(result);
        }

        [Theory]
        [InlineData(AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.Chaps)]
        [InlineData(AllowedPaymentSchemes.Bacs)]
        [InlineData(AllowedPaymentSchemes.Chaps)]
        public void DoesNotAllowFasterFailsValidation(AllowedPaymentSchemes schemes)
        {
            FasterPaymentValidator sut = GetSystemUnderTest();
            Account account = _fixture.Create<Account>();
            account.Balance = decimal.MaxValue;
            account.AllowedPaymentSchemes = schemes;

            bool result = sut.ValidatePayment(account, 10);

            Assert.False(result);
        }

        [Theory]
        [InlineData(10, 100)]
        [InlineData(-100, 1)]
        [InlineData(1234567, 123456789)]
        public void LowBalanceFailsValidation(decimal balance, decimal amount)
        {
            FasterPaymentValidator sut = GetSystemUnderTest();
            Account account = _fixture.Create<Account>();
            account.Balance = balance;
            account.AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps;

            bool result = sut.ValidatePayment(account, amount);

            Assert.False(result);
        }

        [Theory]
        [InlineData(AllowedPaymentSchemes.Chaps | AllowedPaymentSchemes.FasterPayments | AllowedPaymentSchemes.Bacs)]
        [InlineData(AllowedPaymentSchemes.FasterPayments | AllowedPaymentSchemes.Bacs)]
        [InlineData(AllowedPaymentSchemes.FasterPayments | AllowedPaymentSchemes.Chaps)]
        [InlineData(AllowedPaymentSchemes.FasterPayments)]
        public void AllowsFasterPassessValidation(AllowedPaymentSchemes schemes)
        {
            FasterPaymentValidator sut = GetSystemUnderTest();
            Account account = _fixture.Create<Account>();
            account.Balance = decimal.MaxValue;
            account.AllowedPaymentSchemes = schemes;

            bool result = sut.ValidatePayment(account, 10);

            Assert.True(result);
        }

        private FasterPaymentValidator GetSystemUnderTest()
        {
            return new FasterPaymentValidator();
        }
    }
}
