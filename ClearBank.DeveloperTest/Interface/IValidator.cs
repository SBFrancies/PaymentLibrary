using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClearBank.DeveloperTest.Models;

namespace ClearBank.DeveloperTest.Interface
{
    public interface IValidator
    {
        bool ValidatePayment(Account account, decimal amount);
    }
}
