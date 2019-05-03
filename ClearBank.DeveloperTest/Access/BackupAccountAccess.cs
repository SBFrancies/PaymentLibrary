using System;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Threading;
using System.Threading.Tasks;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Exceptions;
using ClearBank.DeveloperTest.Interface;
using ClearBank.DeveloperTest.Models;

namespace ClearBank.DeveloperTest.Access
{
    public class BackupAccountAccess : IAccountAccess
    {
        private readonly Func<AccountDbContext> _dbContextFactory;

        public BackupAccountAccess(Func<AccountDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Account> GetAccount(string accountNumber, CancellationToken cancellationToken = default )
        {
            using (AccountDbContext context = _dbContextFactory())
            {
                BackupAccountEntity entity = await
                    context.BackUpAccounts.SingleOrDefaultAsync(a => a.AccountNumber == accountNumber, cancellationToken);

                return MapEntityToAccount(entity);
            }
        }

        public async Task UpdateAccount(Account account, CancellationToken cancellationToken = default)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            using (AccountDbContext context = _dbContextFactory())
            {
                AccountEntity entity = await
                    context.BackUpAccounts.SingleOrDefaultAsync(a => a.AccountNumber == account.AccountNumber, cancellationToken);

                if (entity == null)
                {
                    throw new AccountNotFoundException($"Account number {account.AccountNumber} not found");
                }

                entity.Balance = account.Balance;
                entity.Status = account.Status;
                entity.AllowedPaymentSchemes = account.AllowedPaymentSchemes;

                await context.SaveChangesAsync(cancellationToken);
            }
        }

        private Account MapEntityToAccount(BackupAccountEntity entity)
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
