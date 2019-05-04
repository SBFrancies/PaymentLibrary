using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClearBank.DeveloperTest.Interface;
using ClearBank.DeveloperTest.Services;
using Moq;

namespace ClearBank.DeveloperTest.Tests.ServiceTests
{
    public class PaymentServiceTests
    {
        private readonly Mock<IAccountAccess> _mockAccountAccess = new Mock<IAccountAccess>();
        private readonly Mock<IValidationFactory> _mockValidationFactory = new Mock<IValidationFactory>();



        private PaymentService GetSystemUnderTest()
        {
            return new PaymentService(_mockAccountAccess.Object, _mockValidationFactory.Object);
        }
    }
}
