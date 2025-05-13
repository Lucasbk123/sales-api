using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSale;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;

public static class GetPaginatedSaleCommandTestData
{
    public static PaginatedCommandResult<GetPaginatedSaleCommandResult> ToMappePaginatedCommandResult(this IEnumerable<Sale> sales,
        int page, int pageSize, int totalRows) =>
     new(sales.Select(s => new GetPaginatedSaleCommandResult
     {
         Id = s.Id,
         BranchName = s.BranchName,
         CustomerName = s.CustomerName,
         TotalValue = s.TotalValue,
         Status = s.Status,
         CreatedAt = s.CreatedAt,
         Number = s.Number
     }), page, pageSize, totalRows);
}
