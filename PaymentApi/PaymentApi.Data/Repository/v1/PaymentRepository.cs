using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaymentApi.Data.Database;
using PaymentApi.Domain;

namespace PaymentApi.Data.Repository.v1
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(PaymentContext orderContext) : base(orderContext)
        {
        }

        public async Task<Payment> GetPaymentByIdAsync(Guid paymentId, CancellationToken cancellationToken)
        {
            return await PaymentContext.Payment.FirstOrDefaultAsync(x => x.Id == paymentId, cancellationToken);
        }
    }
}
