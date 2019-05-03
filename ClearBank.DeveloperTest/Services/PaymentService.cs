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

        public async Task<MakePaymentResult> MakePayment(MakePaymentRequest request, CancellationToken cancellationToken = default)
        {
            Account debtorAccount = await _accountAccess.GetAccount(request.DebtorAccountNumber, cancellationToken);
            IValidator validator = _validationFactory.GetValidator(request.PaymentScheme);

            bool valid = validator.ValidatePayment(debtorAccount, request.Amount);

            if (valid)
            {
                Account creditorAccount = await _accountAccess.GetAccount(request.CreditorAccountNumber, cancellationToken);

                debtorAccount.Balance -= request.Amount;
                creditorAccount.Balance += request.Amount;

                await _accountAccess.UpdateAccount(debtorAccount, cancellationToken);
                await _accountAccess.UpdateAccount(creditorAccount, cancellationToken);
            }

            return new MakePaymentResult {Success = valid};
        }
    }
}
