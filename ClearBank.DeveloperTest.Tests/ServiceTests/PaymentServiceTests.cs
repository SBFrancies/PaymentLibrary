using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using ClearBank.DeveloperTest.Enums;
using ClearBank.DeveloperTest.Interface;
using ClearBank.DeveloperTest.Models;
using ClearBank.DeveloperTest.Services;
using Moq;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.ServiceTests
{
    public class PaymentServiceTests
    {
        private readonly Mock<IAccountAccess> _mockAccountAccess = new Mock<IAccountAccess>();
        private readonly Mock<IValidationFactory> _mockValidationFactory = new Mock<IValidationFactory>();
        private readonly Mock<IValidator> _mockValidator = new Mock<IValidator>();
        private readonly Fixture _fixture = new Fixture();
        private readonly MakePaymentRequest _paymentRequest;

        public PaymentServiceTests()
        {
            _paymentRequest = _fixture.Create<MakePaymentRequest>();
        }

        [Fact]
        public void WhenValidatorReturnsFalseResultIsFalse()
        {
            _mockValidator.Setup(a => a.ValidatePayment(It.IsAny<Account>(), It.IsAny<decimal>())).Returns(false);

            PaymentService sut = GetSystemUnderTest();

            MakePaymentResult result = sut.MakePayment(_paymentRequest);

            _mockAccountAccess.Verify(a => a.GetAccount(_paymentRequest.DebtorAccountNumber), Times.Once);
            _mockAccountAccess.Verify(a => a.UpdateAccount(It.IsAny<Account>()), Times.Never);

            Assert.False(result.Success);
        }

        [Fact]
        public void WhenValidatorReturnsTrueResultIsTrue()
        {
            _mockValidator.Setup(a => a.ValidatePayment(It.IsAny<Account>(), It.IsAny<decimal>())).Returns(true);
            _mockAccountAccess
                .Setup(a => a.GetAccount(_paymentRequest.DebtorAccountNumber))
                .Returns(_fixture.Create<Account>());
            _mockAccountAccess
                .Setup(a => a.GetAccount(_paymentRequest.CreditorAccountNumber))
                .Returns(_fixture.Create<Account>());

            PaymentService sut = GetSystemUnderTest();

            MakePaymentResult result =  sut.MakePayment(_paymentRequest);

            _mockAccountAccess.Verify(a => a.GetAccount(_paymentRequest.DebtorAccountNumber), Times.Once);
            _mockAccountAccess.Verify(a => a.UpdateAccount(It.IsAny<Account>()), Times.Once);

            Assert.True(result.Success);
        }

        private PaymentService GetSystemUnderTest()
        {
            _mockValidationFactory.Setup(a => a.GetValidator(It.IsAny<PaymentScheme>())).Returns(_mockValidator.Object);
            return new PaymentService(_mockAccountAccess.Object, _mockValidationFactory.Object);
        }
    }
}
