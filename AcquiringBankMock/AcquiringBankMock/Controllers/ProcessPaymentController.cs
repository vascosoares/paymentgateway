using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AcquiringBankMock.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessPaymentController : ControllerBase
    {
        private readonly ILogger<ProcessPaymentController> _logger;

        public ProcessPaymentController(ILogger<ProcessPaymentController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///     Action to create a new payment in the database.
        /// </summary>
        /// <param name="payment">Model to create a new payment</param>
        /// <returns>Returns the created payment</returns>
        /// <response code="200">Returned if the payment was created</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<Payment>> Payment(Payment paymentModel)
        {
            try
            {
                return new Payment
                {
                    Id = new Guid(),
                    CardNumber = paymentModel.CardNumber,
                    ExpiryMonth = 12,
                    ExpiryYear = 2021,
                    Amount = 12.60,
                    Currency = "EUR"
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
