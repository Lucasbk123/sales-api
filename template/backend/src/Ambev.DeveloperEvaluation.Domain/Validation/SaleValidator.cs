using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(sale => sale.CustomerId).NotEmpty();
        RuleFor(sale => sale.BranchId).NotEmpty();
        RuleFor(sale => sale.BranchName).SetValidator(new BranchNameValidator());
        RuleFor(sale => sale.CustomerName).SetValidator(new CustomerNameValidator());
        RuleFor(sale => sale.Items).NotEmpty();

        RuleFor(sale => sale.Status)
            .NotEqual(Enums.SaleStatus.Unknown);

        RuleFor(sale => sale.Items)
         .Must(items => !(items.GroupBy(item => item.ProductId).Where(x => x.Count() > ValidationConstant.ItemRepatLimit).Any()))
         .WithMessage($"The maximum number of duplicate products allowed is {ValidationConstant.ItemRepatLimit}");

        RuleForEach(x => x.Items).SetValidator(new SaleItemValidator());

    }
}

public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        RuleFor(item => item.SaleId).NotEmpty();
        RuleFor(item => item.ProductId).NotEmpty();
        RuleFor(item => item.ProductName).SetValidator(new ProductNameValidator());
        RuleFor(item => item.UnitPrice).GreaterThan(0);
        RuleFor(item => item.Quantity).SetValidator(x => new ProductQuantityValidator());
    }
}