﻿using System;

namespace PaymentGatewayApi.Domain
{
    public partial class Payment
    {
        public Guid Id { get; set; }
        public string CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public string CVV { get; set; }
    }
}
