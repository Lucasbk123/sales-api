using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class BranchNameValidator : AbstractValidator<string>
{
    public BranchNameValidator()
    {
        RuleFor(branchName => branchName).NotEmpty().MaximumLength(100);
    }
}