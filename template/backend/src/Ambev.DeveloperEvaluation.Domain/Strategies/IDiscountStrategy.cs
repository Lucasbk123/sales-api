namespace Ambev.DeveloperEvaluation.Domain.Strategies;

public interface IDiscountStrategy
{
    decimal Calculate(Product product);
}
                                       
public record Product(decimal UnitPrice, short Quantity);