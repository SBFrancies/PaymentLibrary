using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClearBank.DeveloperTest.Enums;

namespace ClearBank.DeveloperTest.Data
{
    public class AccountEntity
    {
        public string AccountNumber { get; set; }

        public decimal Balance { get; set; }

        public AccountStatus Status { get; set; }

        public AllowedPaymentSchemes AllowedPaymentSchemes { get; set; }
    }

    public class BackupAccountEntity : AccountEntity
    {
        
    }
}
