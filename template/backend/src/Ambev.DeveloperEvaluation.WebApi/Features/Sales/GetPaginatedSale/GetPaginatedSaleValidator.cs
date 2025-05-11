using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetPaginatedSale
{
    public class GetPaginatedSaleValidator  : AbstractValidator<GetPaginatedSaleRequest>
    {
        public GetPaginatedSaleValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
        }
    }
}
