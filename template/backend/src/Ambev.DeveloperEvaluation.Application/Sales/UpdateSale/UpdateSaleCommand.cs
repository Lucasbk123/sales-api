using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleCommand : IRequest
{
    public Guid Id { get; set; }
    public string CustomerName { get; set; }

    public Guid CustomerId { get; set; }

    public Guid BranchId { get; set; }

    public string BranchName { get; set; }
    public IEnumerable<UpdateSaleItemCommand> Items { get; set; }
}

public class UpdateSaleItemCommand
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    public decimal UnitPrice { get; set; }

    public short Quantity { get; set; }
}
