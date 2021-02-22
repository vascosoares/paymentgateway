using PaymentGatewayApi.Domain;

namespace PaymentGatewayApi.Messaging.Send.Sender.v1
{
    public interface IPaymentCreateSender
    {
        void SendPaymentCreate(Payment payment);
    }
}
