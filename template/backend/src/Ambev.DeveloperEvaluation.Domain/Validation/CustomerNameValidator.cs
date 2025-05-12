using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class CustomerNameValidator : AbstractValidator<string>
{
    public CustomerNameValidator()
    {
        RuleFor(customerName => customerName).NotEmpty().MaximumLength(100);
    }
}
