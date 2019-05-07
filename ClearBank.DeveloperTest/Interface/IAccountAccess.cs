using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ClearBank.DeveloperTest.Models;

namespace ClearBank.DeveloperTest.Interface
{
    public interface IAccountAccess
    {
        Account GetAccount(string accountNumber);

        void UpdateAccount(Account account);
    }
}
