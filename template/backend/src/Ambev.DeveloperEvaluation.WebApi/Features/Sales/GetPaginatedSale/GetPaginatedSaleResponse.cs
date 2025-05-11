using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetPaginatedSale
{
    public class GetPaginatedSaleResponse
    {
        public Guid Id { get; set; }
        public long Number { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CustomerName { get; set; }

        public decimal TotalValue { get; set; }

        public string BranchName { get; set; }

        public SaleStatus Status { get; set; }
    }

}