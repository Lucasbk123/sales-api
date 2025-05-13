using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleCommandValidator()
        {
            RuleFor(sale => sale.CustomerId).NotEmpty();
            RuleFor(sale => sale.BranchId).NotEmpty();
            RuleFor(sale => sale.BranchName).NotEmpty().MaximumLength(100);
            RuleFor(sale => sale.CustomerName).NotEmpty().MaximumLength(100);
            RuleFor(sale => sale.Items).NotEmpty()
            .DependentRules(() =>
            {
                RuleFor(sale => sale.Items)
                 .Must(items => !(items.GroupBy(item => item.ProductId).Where(x => x.Count() > ValidationConstant.ItemRepatLimit).Any()))
                 .WithMessage($"O número máximo permitido de produtos repetidos é {ValidationConstant.ItemRepatLimit}");
            });

            RuleForEach(x => x.Items).SetValidator(new UpdateSaleItemCommandValidator());
        }
    }

    public class UpdateSaleItemCommandValidator : AbstractValidator<UpdateSaleItemCommand>
    {
        public UpdateSaleItemCommandValidator()
        {
            RuleFor(item => item.ProductId).NotEmpty();
            RuleFor(item => item.ProductName).MaximumLength(500).NotEmpty();
            RuleFor(item => item.UnitPrice).GreaterThan(0);
            RuleFor(item => item.Quantity).SetValidator(x => new ProductQuantityValidator());
        }
    }
}
