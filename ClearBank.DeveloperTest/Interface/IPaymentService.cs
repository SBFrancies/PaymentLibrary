using System.Threading;
using System.Threading.Tasks;
using ClearBank.DeveloperTest.Models;

namespace ClearBank.DeveloperTest.Interface
{
    public interface IPaymentService
    {
        MakePaymentResult MakePayment(MakePaymentRequest request);
    }
}
