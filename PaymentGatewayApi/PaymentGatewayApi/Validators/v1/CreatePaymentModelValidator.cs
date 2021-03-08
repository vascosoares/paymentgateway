using PaymentGatewayApi.Models.v1;
using FluentValidation;

namespace PaymentGatewayApi.Validators.v1
{
    public class CreatePaymentModelValidator : AbstractValidator<CreatePaymentModel>
    {
        public CreatePaymentModelValidator()
        {
            RuleFor(x => x.CardNumber)
                .NotNull()
                .WithMessage("The Card Number must be 16 digits long");
            RuleFor(x => x.CardNumber)
                .MinimumLength(16).
                WithMessage("The Card Number must be 16 digits long");
            RuleFor(x => x.CardNumber)
                .MaximumLength(16).
                WithMessage("The Card Number must be 16 digits long");
            RuleFor(x => x.CardNumber)
                .CreditCard()
                .WithMessage("The Card Number is not valid");
        }
    }
}
