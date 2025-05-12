using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.PatchSaleItemCancel;

public class PatchSaleItemCancelCommandValidator  : AbstractValidator<PatchSaleItemCancelCommand>
{
    public PatchSaleItemCancelCommandValidator()
    {
        RuleFor(item => item.SaleId).NotEmpty();
        RuleFor(item => item.ProductId).NotEmpty();
    }
}
