using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSale;

public class GetPaginatedSaleCommandResult
{
    public Guid Id { get; set; }
    public long Number { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CustomerName { get; set; }

    public decimal TotalValue { get; set; }

    public string BranchName { get; set; }

    public SaleStatus Status { get; set; }
}