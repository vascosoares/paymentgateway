using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentApi.Models.v1
{
    public class PaymentModel
    {
        [Required] 
        public Guid PaymentGuid { get; set; }

        [Required] 
        public string CardNumber { get; set; }

        [Required]
        public int ExpiryMonth { get; set; }

        [Required]
        public int ExpiryYear { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public string CVV { get; set; }
    }
}
