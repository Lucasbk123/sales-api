using Ambev.DeveloperEvaluation.Domain.Strategies;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Strategies.TestData;

public static class QuantityRangeDiscountStrategyTestData
{
    public static List<DiscountRangeParametres> GetDiscountRangeParametres()
        => [new (4,9,0.1M),
            new (10,20,0.2M),
            new (20,90,0.3M)];

}
