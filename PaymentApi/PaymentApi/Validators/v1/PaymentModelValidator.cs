using FluentValidation;
using PaymentApi.Models.v1;

namespace PaymentApi.Validators.v1
{
    public class PaymentModelValidator : AbstractValidator<PaymentModel>
    {
        public PaymentModelValidator()
        {
            RuleFor(x => x.PaymentGuid)
                .NotNull()
                .WithMessage("The payment guid should not be null");
            RuleFor(x => x.CardNumber)
                .MinimumLength(16).WithMessage("The card number must be 16 character long");
        }
    }
}
