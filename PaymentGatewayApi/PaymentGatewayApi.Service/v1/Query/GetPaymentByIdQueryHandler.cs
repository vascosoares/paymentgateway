using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
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
            Payment payment;

            using (var httpClient = new HttpClient())
            {
                // (TODO VS) Remote Service should be resolved somehow. Configuration? Other Service? Problems comunicating in docker and TLS certificates
                string url = $"https://localhost:44394/v1/Payment/" + request.Id.ToString();

                using (HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    payment = JsonConvert.DeserializeObject<Payment>(apiResponse);
                }
            }

            return payment;
        }
    }
}
