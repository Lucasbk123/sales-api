using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.PatchSaleItem;

public class PatchSaleItemRequestValidator : AbstractValidator<PatchSaleItemRequest>
{
    public PatchSaleItemRequestValidator()
    {
        RuleFor(item => item.ProductName).SetValidator(new ProductNameValidator());
        RuleFor(item => item.UnitPrice).GreaterThan(0);
        RuleFor(item => item.Quantity).SetValidator(x => new ProductQuantityValidator(x.ProductName));
    }
}
