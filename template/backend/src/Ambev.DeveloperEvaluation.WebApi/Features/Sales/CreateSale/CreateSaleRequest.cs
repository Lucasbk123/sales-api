namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequest
{
    public string CustomerName { get; set; }
    public Guid CustomerId { get; set; }

    public Guid BranchId { get; set; }
    public string BranchName { get; set; }

    public IEnumerable<CreateSaleItemRequest> Items { get; set; }
}

public class CreateSaleItemRequest
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    public decimal UnitPrice { get; set; }

    public short Quantity { get; set; }
}
