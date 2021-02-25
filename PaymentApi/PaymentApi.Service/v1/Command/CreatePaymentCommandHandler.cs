using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
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
            Payment payment;

            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(request.Payment);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                using (HttpResponseMessage response = await httpClient.PostAsync("https://127.0.0.1:44379/ProcessPayment", byteContent))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    payment = JsonConvert.DeserializeObject<Payment>(apiResponse);
                }
            }

            payment = await _paymentRepository.AddAsync(request.Payment);

            return payment;
        }
    }
}
