using System;
using System.Diagnostics;
using MediatR;
using PaymentApi.Service.v1.Command;
using PaymentApi.Service.v1.Models;
using PaymentApi.Service.v1.Query;

namespace PaymentApi.Service.v1.Services
{
    public class CreatePaymentService : ICreatePaymentService
    {
        private readonly IMediator _mediator;

        public CreatePaymentService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async void CreatePayment(CreatePaymentModel createPaymentModel)
        {
            try
            {
                await _mediator.Send(new CreatePaymentCommand
                {
                    Payment = new Domain.Payment
                    { 
                        Id = createPaymentModel.Id,
                        PaymentState = 1,
                        Amount = createPaymentModel.Amount,
                        CardNumber = createPaymentModel.CardNumber,
                        Currency = createPaymentModel.Currency,
                        CVV = createPaymentModel.CVV,
                        ExpiryMonth = createPaymentModel.ExpiryMonth,
                        ExpiryYear = createPaymentModel.ExpiryYear
                    }
                });
            }
            catch (Exception ex)
            {
                // log an error message here

                Debug.WriteLine(ex.Message);
            }
        }
    }
}
