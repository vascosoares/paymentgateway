using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGatewayApi.Domain;
using PaymentGatewayApi.Models.v1;

namespace PaymentGatewayApi.Controllers.v1
{
    public class PaymentGatewayController : ControllerBase
    {
        private readonly ILogger<PaymentGatewayController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PaymentGatewayController(IMapper mapper, IMediator mediator, ILogger<PaymentGatewayController> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        ///     Action to create a new payment.
        /// </summary>
        /// <param name="paymentGatewayModel">Model to create a new payment</param>
        /// <returns>Returns the created payment</returns>
        /// <response code="200">Returned if the payment was created</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<Payment>> Payment(PaymentGatewayModel paymentGatewayModel)
        {
            try
            {
                return await _mediator.Send(new CreatePaymentCommand
                {
                    Payment = _mapper.Map<Payment>(paymentGatewayModel)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///     Action to retrieve a payment by id.
        /// </summary>
        /// <param name="id">The id of the payment</param>
        /// <returns>Returns the payment</returns>
        /// <response code="200">Returned if the payment id was found</response>
        /// <response code="400">Returned if the payment could not be found with the provided id</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> Payment(Guid id)
        {
            try
            {
                return await _mediator.Send(new GetPaymentByIdQuery
                {
                    Id = id
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
