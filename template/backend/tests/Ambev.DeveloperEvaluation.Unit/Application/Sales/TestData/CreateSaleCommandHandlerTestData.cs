using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;

public static class CreateSaleCommandHandlerTestData
{
    public static CreateSaleCommand ToMapperCreateSaleCommand(this Sale sale) =>
       new()
       {
           BranchId = sale.BranchId,
           BranchName = sale.BranchName,
           CustomerId = sale.CustomerId,
           CustomerName = sale.CustomerName,
           Items = sale.Items.Select(s => new CreateSaleItemCommand
           {
               ProductId = s.ProductId,
               ProductName = s.ProductName,
               Quantity = s.Quantity,
               UnitPrice = s.UnitPrice
           })
       };

}
