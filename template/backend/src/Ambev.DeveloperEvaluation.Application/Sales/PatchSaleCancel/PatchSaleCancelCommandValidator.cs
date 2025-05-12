using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.PatchSaleCancel
{
    public class PatchSaleCancelCommandValidator : AbstractValidator<PatchSaleCancelCommand>
    {
        public PatchSaleCancelCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
