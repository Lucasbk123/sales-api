using Ambev.DeveloperEvaluation.Application.Sales.PatchSaleItem;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;

public static class PatchSaleItemCommandHandlerTestData
{
    public static PatchSaleItemCommand ToMapperCommand(this SaleItem saleItem,Guid saleId) =>
         new()
         {
            ProductId = saleItem.ProductId,
            SaleId = saleId,
            Quantity = 10,
            UnitPrice = 25
        };

}
