using System;
using MediatR;
using PaymentApi.Domain;

namespace PaymentApi.Service.v1.Query
{
    public class GetPaymentByIdQuery : IRequest<Payment>
    {
        public Guid Id { get; set; }
    }
}
