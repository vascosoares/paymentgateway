using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PaymentApi.Domain;

namespace PaymentApi.Data.Repository.v1
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<Payment> GetPaymentByIdAsync(Guid paymentId, CancellationToken cancellationToken);
    }
}
