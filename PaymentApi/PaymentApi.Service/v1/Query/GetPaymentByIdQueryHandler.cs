using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaymentApi.Data.Repository.v1;
using PaymentApi.Domain;

namespace PaymentApi.Service.v1.Query
{
    public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, Payment>
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetPaymentByIdQueryHandler(IPaymentRepository orderRepository)
        {
            _paymentRepository = orderRepository;
        }

        public async Task<Payment> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _paymentRepository.GetPaymentByIdAsync(request.Id, cancellationToken);
        }
    }
}
