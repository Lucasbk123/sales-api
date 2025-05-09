using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.PatchSaleItemCancel;

public class PatchSaleItemCancelRequestValidator : AbstractValidator<PatchSaleItemCancelRequest>
{
    public PatchSaleItemCancelRequestValidator()
    {
        RuleFor(item => item.SaleId).NotEmpty();
        RuleFor(item => item.ProductId).NotEmpty();
    }
}