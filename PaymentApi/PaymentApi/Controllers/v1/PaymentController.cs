using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentApi.Domain;
using PaymentApi.Models.v1;
using PaymentApi.Service.v1.Command;
using PaymentApi.Service.v1.Query;

namespace PaymentApi.Controllers.v1
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PaymentController(IMapper mapper, IMediator mediator, ILogger<PaymentController> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        ///     Action to create a new payment in the database.
        /// </summary>
        /// <param name="paymentModel">Model to create a new payment</param>
        /// <returns>Returns the created payment</returns>
        /// <response code="200">Returned if the payment was created</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<Payment>> Payment(PaymentModel paymentModel)
        {
            try
            {
                return await _mediator.Send(new CreatePaymentCommand
                {
                    Payment = _mapper.Map<Payment>(paymentModel)
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
