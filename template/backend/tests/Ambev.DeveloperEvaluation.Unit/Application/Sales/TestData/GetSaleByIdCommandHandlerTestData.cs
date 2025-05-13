using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;

public static class GetSaleByIdCommandHandlerTestData
{
    public static GetSaleByIdCommandResult ToMapperetSaleItemByIdCommandResult(this Sale sale) =>
     new()
     {
         Id = sale.Id,
         BranchName = sale.BranchName,
         CustomerName = sale.CustomerName,
         TotalValue = sale.TotalValue,
         Items = sale.Items.Select(s => new GetSaleItemByIdCommandResult
         {
             ProductId = s.ProductId,
             ProductName = s.ProductName,
             Quantity = s.Quantity,
             UnitPrice = s.UnitPrice,
             Discount = s.Discount,
             TotalPrice = s.GetTotalValue()
         })
     };
}
