using PaymentApi.Service.v1.Models;

namespace PaymentApi.Service.v1.Services
{
    public interface ICreatePaymentService
    {
        void CreatePayment(CreatePaymentModel createPaymentModel);
    }
}
