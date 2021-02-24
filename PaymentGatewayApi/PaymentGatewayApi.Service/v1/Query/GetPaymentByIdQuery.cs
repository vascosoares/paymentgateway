using System;
using PaymentGatewayApi.Domain;
using MediatR;

namespace PaymentGatewayApi.Service.v1.Query
{
    public class GetPaymentByIdQuery : IRequest<Payment>
    {
        public Guid Id { get; set; }
    }
}
