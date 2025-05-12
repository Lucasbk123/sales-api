namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleRequest
{
    public string CustomerName { get; set; }

    public Guid CustomerId { get; set; }

    public Guid BranchId { get; set; }

    public string BranchName { get; set; }
    public IEnumerable<UpdateSaleItemRequest> Items { get; set; }
}

public class UpdateSaleItemRequest
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    public decimal UnitPrice { get; set; }

    public short Quantity { get; set; }
}
