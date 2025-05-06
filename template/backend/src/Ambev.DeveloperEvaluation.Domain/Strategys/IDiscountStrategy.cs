namespace Ambev.DeveloperEvaluation.Domain.Strategys;

public interface IDiscountStrategy
{
    decimal Calculate(Product product);
}
                                       
public record Product(decimal UnitPrice, short Quantity);