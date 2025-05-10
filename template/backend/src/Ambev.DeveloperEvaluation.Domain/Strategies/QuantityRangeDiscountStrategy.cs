using Microsoft.Extensions.Options;

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

        return discountParametre != null ? (product.UnitPrice * product.Quantity) * discountParametre.DiscountPercent : DiscountPriceDefault;
    }
}

public record DiscountRangeParametres(short Min,short Max,decimal DiscountPercent);
