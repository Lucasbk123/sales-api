using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class ProductQuantityValidator : AbstractValidator<short>
    {
        const short QuantityMin = 1;

        const short QuantityMax = 20;

        public ProductQuantityValidator()
        {
            RuleFor(quantity => quantity)
                .GreaterThanOrEqualTo(QuantityMin)
                .LessThanOrEqualTo(QuantityMax);
        }
    }
}