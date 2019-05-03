using System.Threading;
using System.Threading.Tasks;
using ClearBank.DeveloperTest.Models;

namespace ClearBank.DeveloperTest.Interface
{
    public interface IPaymentService
    {
        Task<MakePaymentResult> MakePayment(MakePaymentRequest request, CancellationToken cancellationToken = default);
    }
}
