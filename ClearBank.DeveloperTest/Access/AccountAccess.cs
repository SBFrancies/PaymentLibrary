using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Exceptions;
using ClearBank.DeveloperTest.Interface;
using ClearBank.DeveloperTest.Models;

namespace ClearBank.DeveloperTest.Access
{
    public class AccountAccess : IAccountAccess
    {
        private readonly Func<AccountDbContext> _dbContextFactory;

        public AccountAccess(Func<AccountDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Account GetAccount(string accountNumber)
        {
            using (AccountDbContext context = _dbContextFactory())
            {
                AccountEntity entity =
                    context.Accounts.SingleOrDefault(a => a.AccountNumber == accountNumber);

                return MapEntityToAccount(entity);
            }
        }

        public void UpdateAccount(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            using (AccountDbContext context = _dbContextFactory())
            {
                AccountEntity entity = 
                    context.Accounts.SingleOrDefault(a => a.AccountNumber == account.AccountNumber);

                if (entity == null)
                {
                    throw new AccountNotFoundException($"Account number {account.AccountNumber} not found");
                }

                entity.Balance = account.Balance;
                entity.Status = account.Status;
                entity.AllowedPaymentSchemes = account.AllowedPaymentSchemes;

                context.SaveChanges();
            }
        }

        private Account MapEntityToAccount(AccountEntity entity)
        {
            return entity == null ? null :
                new Account
                {
                    AccountNumber = entity.AccountNumber,
                    Status = entity.Status,
                    AllowedPaymentSchemes = entity.AllowedPaymentSchemes,
                    Balance = entity.Balance,
                };
        }
    }
}
