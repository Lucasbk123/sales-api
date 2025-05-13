using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSale;

public class GetPaginatedSaleCommandValidator : AbstractValidator<GetPaginatedSaleCommand>
{
    public GetPaginatedSaleCommandValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0);
    }
}
