using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class ProductQuantityValidator : AbstractValidator<short>
    {
        const short QuantityMin = 1;

        const short QuantityMax = 20;

        public ProductQuantityValidator(string productName)
        {
            RuleFor(quantity => quantity)
                .GreaterThanOrEqualTo(QuantityMin)
                .WithMessage(x => $"Produto {productName} deve ter Quantidade superior a 0")
                .LessThanOrEqualTo(QuantityMax)
                .WithMessage(x => $"Produto {productName} deve ter Quantidade inferior ou igual a {QuantityMax}");
        }
    }
}