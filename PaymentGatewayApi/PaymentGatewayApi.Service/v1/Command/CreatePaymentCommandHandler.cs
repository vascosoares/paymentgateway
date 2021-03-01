using System.Threading;
using System.Threading.Tasks;
using PaymentGatewayApi.Domain;
using PaymentGatewayApi.Messaging.Send.Sender.v1;
using MediatR;

namespace PaymentGatewayApi.Service.v1.Command
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Payment>
    {
        private readonly IPaymentCreateSender _paymentCreateSender;

        public CreatePaymentCommandHandler(IPaymentCreateSender paymentCreateSender)
        {
            _paymentCreateSender = paymentCreateSender;
        }

        public async Task<Payment> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            return await Task.Run(() => {
                request.Payment.Id = System.Guid.NewGuid();
                _paymentCreateSender.SendPaymentCreate(request.Payment);
                request.Payment.CardNumber = string.Concat("xxxxxxxxxxxx", request.Payment.CardNumber.Substring(12));
                return request.Payment; 
            });
        }
    }
}
