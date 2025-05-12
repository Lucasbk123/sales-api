using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommand : IRequest<CreateSaleCommandResult>
{
    public string CustomerName { get; set; }
    public Guid CustomerId { get; set; }

    public Guid BranchId { get; set; }
    public string BranchName { get; set; }

    public IEnumerable<CreateSaleItemCommand> Items { get; set; }

}

public class CreateSaleItemCommand
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    public decimal UnitPrice { get; set; }

    public short Quantity { get; set; }
}
