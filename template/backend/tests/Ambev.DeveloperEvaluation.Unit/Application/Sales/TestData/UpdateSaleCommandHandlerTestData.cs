using Ambev.DeveloperEvaluation.Application.Sales.PatchSaleItem;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;

public static class UpdateSaleCommandHandlerTestData
{
    public static UpdateSaleCommand ToMapperCommand(this Sale sale) =>
     new()
     {
         Id = sale.Id,
         BranchId = sale.BranchId,
         BranchName = sale.BranchName,
         CustomerId = sale.CustomerId,
         CustomerName = sale.CustomerName,
         Items =  sale.Items.Select(s => new UpdateSaleItemCommand
         {
             ProductId = s.ProductId,
             ProductName = s.ProductName,
             Quantity = s.Quantity,
             UnitPrice  = s.UnitPrice
         })
     };

}