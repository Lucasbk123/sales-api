using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class ProductNameValidator : AbstractValidator<string>
{
    public ProductNameValidator()
    {
        RuleFor(branchName => branchName).NotEmpty().MaximumLength(100);
    }
}
