using Microsoft.Extensions.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ambev.DeveloperEvaluation.Domain.Strategies;

public class QuantityRangeDiscountStrategy : IDiscountStrategy
{
    const decimal DiscountPriceDefault = 0;

    private readonly List<DiscountRangeParametres> _discountRangeParametres;

    public QuantityRangeDiscountStrategy(IOptions<List<DiscountRangeParametres>> discountRangeParametres)
    {
        _discountRangeParametres = discountRangeParametres.Value;
    }

    public decimal Calculate(Product product)
    {
        var discountParametre = _discountRangeParametres
             .Where(x => product.Quantity >= x.Min && product.Quantity <= x.Max)
        .MaxBy(x => x.DiscountPercent);

        return discountParametre != null ?
            Math.Round((product.UnitPrice * product.Quantity) 
            * discountParametre.DiscountPercent, 2, MidpointRounding.AwayFromZero) 
            : DiscountPriceDefault;
    }
}

public record DiscountRangeParametres(short Min,short Max,decimal DiscountPercent);
