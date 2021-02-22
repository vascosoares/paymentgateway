using MediatR;
using PaymentApi.Domain;

namespace PaymentApi.Service.v1.Command
{
    public class CreatePaymentCommand : IRequest<Payment>
    {
        public Payment Payment { get; set; }
    }
}
