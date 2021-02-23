using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaymentApi.Data.Repository.v1;
using PaymentApi.Domain;

namespace PaymentApi.Service.v1.Command
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Payment>
    {
        private readonly IPaymentRepository _paymentRepository;

        public CreatePaymentCommandHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Payment> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            // Call external gateway here!!! (Synch or Asynch?)

            return await _paymentRepository.AddAsync(request.Payment);
        }
    }
}
