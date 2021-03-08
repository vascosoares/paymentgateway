using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PaymentApi.Data.Repository.v1;
using PaymentApi.Service.v1.Options;
using PaymentApi.Domain;

namespace PaymentApi.Service.v1.Command
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Payment>
    {
        private readonly ILogger<CreatePaymentCommandHandler> _logger;
        private readonly IOptions<ExternalBankConfiguration> _externalBankOptions;
        private readonly IPaymentRepository _paymentRepository;

        public CreatePaymentCommandHandler(IPaymentRepository paymentRepository, IOptions<ExternalBankConfiguration> externalBankOptions, ILogger<CreatePaymentCommandHandler> logger)
        {
            _logger = logger;
            _externalBankOptions = externalBankOptions;
            _paymentRepository = paymentRepository;
        }

        public async Task<Payment> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            Payment payment;

            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(request.Payment);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                _logger.LogInformation("Calling External Bank Service with Id: " + request.Payment.Id.ToString());

                // (TODO VS) Remote Service should be resolved somehow. Configuration? Other Service? Problems comunicating in docker and TLS certificates
                using (HttpResponseMessage response = await httpClient.PostAsync(_externalBankOptions.Value.EndPoint, byteContent, cancellationToken))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    payment = JsonConvert.DeserializeObject<Payment>(apiResponse);
                    payment.PaymentGuid = payment.Id;
                    payment.Id = request.Payment.Id;
                    _logger.LogInformation("Received Response from External Bank Service with Id: " + payment.Id.ToString());
                }
            }

            payment = await _paymentRepository.AddAsync(payment);

            return payment;
        }
    }
}
