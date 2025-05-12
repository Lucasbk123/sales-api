using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
        RuleFor(sale => sale.CustomerId).NotEmpty();
        RuleFor(sale => sale.BranchId).NotEmpty();
        RuleFor(sale => sale.BranchName).SetValidator(new BranchNameValidator());
        RuleFor(sale => sale.CustomerName).SetValidator(new CustomerNameValidator());
        RuleFor(sale => sale.Items).NotEmpty();

        RuleFor(sale => sale.Items)
         .Must(items => !(items.GroupBy(item => item.ProductId).Where(x => x.Count() > ValidationConstant.ItemRepatLimit).Any()))
         .WithMessage($"O número máximo permitido de produtos repetidos é {ValidationConstant.ItemRepatLimit}");

        RuleForEach(x => x.Items).SetValidator(new CreateSaleItemRequestValidator());
    }
}

public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
{
    public CreateSaleItemRequestValidator()
    {
        RuleFor(item => item.ProductId).NotEmpty();
        RuleFor(item => item.ProductName).SetValidator(new ProductNameValidator());
        RuleFor(item => item.UnitPrice).GreaterThan(0);
        RuleFor(item => item.Quantity).SetValidator(x => new ProductQuantityValidator(x.ProductName));
    }
}