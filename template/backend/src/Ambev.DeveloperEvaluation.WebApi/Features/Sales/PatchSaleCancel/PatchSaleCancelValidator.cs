using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.PatchSaleCancel
{
    public class PatchSaleCancelValidator : AbstractValidator<PatchSaleCancelRequest>
    {
        public PatchSaleCancelValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
