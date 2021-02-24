using System.Threading;
using System.Threading.Tasks;
using PaymentGatewayApi.Domain;
using MediatR;

namespace PaymentGatewayApi.Service.v1.Query
{
    public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, Payment>
    {
        public GetPaymentByIdQueryHandler()
        {
        }

        public async Task<Payment> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            return await Task.Run(() => new Payment { Id = request.Id }); 
        }
    }
}
