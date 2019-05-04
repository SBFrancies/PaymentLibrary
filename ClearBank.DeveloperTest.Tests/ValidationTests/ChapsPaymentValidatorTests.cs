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
    public class ChapsPaymentValidatorTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void NullAccountFailsValidation()
        {
            ChapsPaymentValidator sut = GetSystemUnderTest();

            bool result = sut.ValidatePayment(null, It.IsAny<decimal>());

            Assert.False(result);
        }

        [Theory]
        [InlineData(AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.FasterPayments)]
        [InlineData(AllowedPaymentSchemes.Bacs)]
        [InlineData(AllowedPaymentSchemes.FasterPayments)]
        public void DoesNotAllowChapsFailsValidation(AllowedPaymentSchemes schemes)
        {
            ChapsPaymentValidator sut = GetSystemUnderTest();
            Account account = _fixture.Create<Account>();
            account.AllowedPaymentSchemes = schemes;

            bool result = sut.ValidatePayment(account, It.IsAny<decimal>());

            Assert.False(result);
        }

        [Theory]
        [InlineData(AccountStatus.Disabled)]
        [InlineData(AccountStatus.InboundPaymentsOnly)]
        public void NonLiveStatusFailsValidation(AccountStatus status)
        {
            ChapsPaymentValidator sut = GetSystemUnderTest();
            Account account = _fixture.Create<Account>();
            account.Status = status;
            account.AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps;

            bool result = sut.ValidatePayment(account, It.IsAny<decimal>());

            Assert.False(result);
        }

        [Theory]
        [InlineData(AllowedPaymentSchemes.Chaps | AllowedPaymentSchemes.FasterPayments | AllowedPaymentSchemes.Bacs)]
        [InlineData(AllowedPaymentSchemes.Chaps | AllowedPaymentSchemes.Bacs)]
        [InlineData(AllowedPaymentSchemes.FasterPayments | AllowedPaymentSchemes.Chaps)]
        [InlineData(AllowedPaymentSchemes.Chaps)]
        public void AllowsChapsPassessValidation(AllowedPaymentSchemes schemes)
        {
            ChapsPaymentValidator sut = GetSystemUnderTest();
            Account account = _fixture.Create<Account>();
            account.Status = AccountStatus.Live;
            account.AllowedPaymentSchemes = schemes;

            bool result = sut.ValidatePayment(account, It.IsAny<decimal>());

            Assert.True(result);
        }

        private ChapsPaymentValidator GetSystemUnderTest()
        {
            return new ChapsPaymentValidator();
        }
    }
}
