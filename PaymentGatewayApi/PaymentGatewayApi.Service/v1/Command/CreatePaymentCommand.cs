using PaymentGatewayApi.Domain;
using MediatR;

namespace PaymentGatewayApi.Service.v1.Command
{
    public class CreatePaymentCommand : IRequest<Payment>
    {
        public Payment Payment { get; set; }
    }
}
