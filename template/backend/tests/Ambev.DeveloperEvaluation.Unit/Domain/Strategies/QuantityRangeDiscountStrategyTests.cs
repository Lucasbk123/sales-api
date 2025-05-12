using Ambev.DeveloperEvaluation.Domain.Strategies;
using Ambev.DeveloperEvaluation.Unit.Domain.Strategies.TestData;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Strategies
{
    public class QuantityRangeDiscountStrategyTests
    {
        [Theory(DisplayName = "Given the unit value of the product and quantity, calculate the discount that this product will have.")]
        [InlineData(30, 10, 60)]
        [InlineData(60, 6, 36)]
        [InlineData(60, 3, 0)]
        [InlineData(60, 4, 24)]
        [InlineData(120,20,720)]
        public void Give_UnitValueAndQuantity_When_Calculate_Then_ShouldCalculateProductDiscount(decimal unitPrice, short quantity, decimal expectedResult)
        {
            //Arrange
            var options = Substitute.For<IOptions<List<DiscountRangeParametres>>>();
            options.Value.Returns(QuantityRangeDiscountStrategyTestData.GetDiscountRangeParametres());

            var discountStrategy = new QuantityRangeDiscountStrategy(options);

            var product = new Product(unitPrice, quantity);

            //Act
            var result = discountStrategy.Calculate(product);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
