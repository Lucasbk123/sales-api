using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    const int ItemRepatLimit = 1;

    public CreateSaleCommandValidator()
    {
        RuleFor(sale => sale.CustomerId).NotEmpty();
        RuleFor(sale => sale.BranchId).NotEmpty();
        RuleFor(sale => sale.BranchName).NotEmpty();
        RuleFor(sale => sale.CustomerName).NotEmpty();
        RuleFor(sale => sale.Items).NotEmpty();

        RuleFor(sale => sale.Items)
         .Must(items => !(items.GroupBy(item => item.ProductId).Where(x => x.Count() > ItemRepatLimit).Any()))
         .WithMessage($"O número máximo permitido de produtos repetidos é {ItemRepatLimit}");

        RuleForEach(x => x.Items).SetValidator(new CreateSaleItemValidator());
    }
}

public class CreateSaleItemValidator : AbstractValidator<CreateSaleItemCommand>
{
    public CreateSaleItemValidator()
    {
        RuleFor(item => item.ProductId).NotEmpty();
        RuleFor(item => item.ProductName).NotEmpty();
        RuleFor(item => item.UnitPrice).GreaterThan(0);
        RuleFor(item => item.Quantity).SetValidator(x => new ProductQuantityValidtor(x.ProductName));
    }
}