using System;
using ClearBank.DeveloperTest.Data;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using ClearBank.DeveloperTest.Access;
using ClearBank.DeveloperTest.Enums;
using ClearBank.DeveloperTest.Interface;
using ClearBank.DeveloperTest.Models;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountAccess _accountAccess;
        private readonly IValidationFactory _validationFactory;
        
        public PaymentService(IAccountAccess accountAccess, IValidationFactory validationFactory)
        {
            _accountAccess = accountAccess;
            _validationFactory = validationFactory;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            Account debtorAccount = _accountAccess.GetAccount(request.DebtorAccountNumber);
            IValidator validator = _validationFactory.GetValidator(request.PaymentScheme);
            bool valid = validator.ValidatePayment(debtorAccount, request.Amount);

            if (valid)
            {
                debtorAccount.Balance -= request.Amount;
                _accountAccess.UpdateAccount(debtorAccount);
            }

            return new MakePaymentResult {Success = valid};
        }
    }
}
