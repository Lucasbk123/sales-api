using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
/// <summary>
/// Validator for GetSaleRequest
/// </summary>
public class GetSaleByIdRequestValidator : AbstractValidator<GetSaleByIdRequest>
{
    /// <summary>
    /// Initializes validation rules for GetSaleRequest
    /// </summary>
    public GetSaleByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}