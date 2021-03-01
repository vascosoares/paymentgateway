using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentApi.Service.v1.Models
{
    public class CreatePaymentModel
    {
        public Guid Id { get; set; }
        public int PaymentState { get; set; }
        public Guid PaymentGuid { get; set; }
        public string CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public string CVV { get; set; }
    }
}
