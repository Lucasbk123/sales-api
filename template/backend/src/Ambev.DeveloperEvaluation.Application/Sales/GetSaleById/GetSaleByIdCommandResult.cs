using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById
{
    public class GetSaleByIdCommandResult
    {
        public Guid Id { get; set; }
        public long Number { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CustomerName { get; set; }

        public decimal TotalValue { get; set; }

        public string BranchName { get; set; }

        public SaleStatus Status { get; set; }

        public IEnumerable<GetSaleItemByIdCommandResult> Items { get; set; }
    }

    public class GetSaleItemByIdCommandResult
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal Discount { get; set; }

        public short Quantity { get; set; }

        public bool Cancelled { get; set; }
    }
}